using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Repository.OkrHistories
{
    public class OkrHistoryRepository : IOkrHistoryRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OkrHistoryRepository(SEP490_G49Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<OkrHistoryDTO>> GetOkrHistories(Guid okrId)
        {

            var orkH = await _context.okrHistories
                   .Where(x => x.OkrId == okrId)
                  .Include(x => x.Attachments)
                  .OrderByDescending(x => x.DateCreated)
                  .Select(x => new OkrHistoryDTO
                  {
                      Id = x.Id,
                      UserName = _context.Users.FirstOrDefault(u => u.Id == x.UserId).UserInformation.FullName,
                      CreatedAt = x.DateCreated,
                      OldProgress = x.OldProgress,
                      NewProgress = x.NewProgress,
                      OldAchieved = x.OldAchieved,
                      NewAchieved = x.NewAchieved,
                      UnitOfTarget = x.UnitOfTarget,
                      OldStatus = x.OldStatus,
                      NewStatus = x.NewStatus,
                      Description = x.Description,
                      Type = x.Type,
                      OkrId = x.OkrId,
                      Attachments = x.Attachments.Select(a => new CommentFileDTO
                      {
                          Id = a.Id,
                          FileName = a.FileName,
                          StoredPath = a.StoredPath,
                          ContentType = a.ContentType,
                          FileSize = a.FileSize,
                          IsImage = a.IsImage
                      }).ToList()
                  })
       .ToListAsync();

            return orkH;
        }


        public async Task<IEnumerable<OKRHistoryCommentDTO>> GetComments(Guid okrId)
        {
            return await _context.okrHistories
                .Where(x => x.OkrId == okrId)
                .Include(x => x.Attachments)
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new OKRHistoryCommentDTO
                {
                    UserName = _context.Users.FirstOrDefault(u => u.Id == x.UserId).UserInformation.FullName,
                    CreatedAt = x.DateCreated,
                    Description = x.Description,
                    OkrId = x.OkrId,
                    Attachments = x.Attachments.Select(a => new CommentFileDTO
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        StoredPath = a.StoredPath,
                        ContentType = a.ContentType,
                        FileSize = a.FileSize,
                        IsImage = a.IsImage
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<Response> AddComment(Guid okrId, string? text, IFormFileCollection? files)
        {


            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var okr = await _context.OKRs.AsNoTracking().FirstOrDefaultAsync(o => o.Id == okrId);
            if (okr == null)
                return new Response { Code = ResponseCode.NotFound, Message = "OKR not found." };

            var historyId = Guid.NewGuid();
            var now = DateTime.UtcNow.AddHours(7);

            var okrHistory = new OkrHistory
            {
                Id = historyId,
                OkrId = okrId,
                Description = text ?? string.Empty,
                UserId = Guid.Parse(userIdStr),
                DateCreated = now,
                Type = "comment",
                Attachments = new List<CommentFile>()
            };

            if (files != null && files.Count > 0)
            {
                var baseFolder = Path.Combine(
                    _webHostEnvironment.ContentRootPath,
                    "Uploads", "Okrs",
                    okrId.ToString(), "Comments", historyId.ToString()
                );
                Directory.CreateDirectory(baseFolder);

                // Cho phép định dạng phổ biến (giống cách bạn check ở UserInformation)
                var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".zip", ".rar" };
                var allowedMimeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg","image/png","image/gif",
            "application/pdf",
            "application/msword",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/vnd.ms-excel",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "application/vnd.ms-powerpoint",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "text/plain","text/csv",
            "application/zip","application/x-rar-compressed","application/x-rar"
        };

                foreach (var file in files)
                {
                    if (file?.Length <= 0) continue;
                    if (file.Length > 25L * 1024 * 1024) // 25MB
                        return new Response { Code = ResponseCode.BadRequest, Message = "File quá lớn (tối đa 25MB)." };

                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!allowedExts.Contains(ext))
                        return new Response { Code = ResponseCode.BadRequest, Message = $"Chỉ cho phép: {string.Join(", ", allowedExts)}." };

                    // (Tuỳ chọn) Nếu muốn chặt MIME như UserInformation
                    if (!string.IsNullOrEmpty(file.ContentType) && !allowedMimeTypes.Contains(file.ContentType))
                    {
                        // Không fail quá gắt: bạn có thể bỏ check MIME nếu cần
                        // return new Response { Code = ResponseCode.BadRequest, Message = "MIME không hợp lệ." };
                    }

                    // Giữ NGUYÊN tên gốc (như UserInformation): đỡ rườm
                    // Nếu lo trùng tên, có thể thêm prefix Guid nếu muốn
                    var safeName = file.FileName;
                    var filePath = Path.Combine(baseFolder, safeName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }

                    // Lưu relative path (bắt đầu bằng /Uploads/...) như UserInformation
                    var relativePath = $"/Uploads/Okrs/{okrId}/Comments/{historyId}/{safeName}";
                    var contentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType;
                    var isImage = contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase) || ext is ".jpg" or ".jpeg" or ".png" or ".gif";

                    okrHistory.Attachments.Add(new CommentFile
                    {
                        Id = Guid.NewGuid(),
                        OkrHistoryId = historyId,
                        FileName = file.FileName,
                        StoredPath = relativePath,
                        ContentType = contentType,
                        FileSize = file.Length,
                        IsImage = isImage,
                        CreatedAt = now
                    });
                }
            }

            _context.okrHistories.Add(okrHistory);
            _context.CommentFiles.AddRange(okrHistory.Attachments);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Add Comment Successfully!" };
        }





        public async Task<Response> DeleteComment(Guid okrHistoryId)
        {
            var okrHistory = await _context.okrHistories
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id == okrHistoryId);
            if (okrHistory == null) return new Response { Code = ResponseCode.NotFound, Message = "Not Found!" };

            foreach (var f in okrHistory.Attachments)
            {
                var phys = Path.Combine(_webHostEnvironment.ContentRootPath, f.StoredPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (System.IO.File.Exists(phys)) System.IO.File.Delete(phys);
            }
            var dir = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Okrs", okrHistory.OkrId.ToString(), "Comments", okrHistory.Id.ToString());
            if (Directory.Exists(dir)) Directory.Delete(dir, recursive: true);

            _context.okrHistories.Remove(okrHistory);
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Delete Comment Successfully!" };
        }


        public async Task<(string PhysicalPath, string ContentType, string FileName)?> GetCommentFilePathAsync(Guid id)
        {
            var fileRec = await _context.CommentFiles.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
            if (fileRec == null) return null;

            var phys = Path.Combine(
                _webHostEnvironment.ContentRootPath,
                fileRec.StoredPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
            );
            if (!System.IO.File.Exists(phys)) return null;

            var ct = string.IsNullOrWhiteSpace(fileRec.ContentType) ? "application/octet-stream" : fileRec.ContentType;
            var name = string.IsNullOrWhiteSpace(fileRec.FileName) ? "download" : fileRec.FileName;
            return (phys, ct, name);
        }

    }
}

using BusinessObjects.DTO.WorkFlow;
using BusinessObjects.Files;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Repository.WorkFlows
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        private readonly SEP490_G49Context _ctx;
        private readonly IFileStorage _files;
        private readonly IHttpContextAccessor _http;

        public WorkFlowRepository(SEP490_G49Context ctx, IFileStorage files, IHttpContextAccessor http)
        {
            _ctx = ctx;
            _files = files;
            _http = http;
        }

        /* ======================= Core actions ======================= */

        // Submit: Draft -> Submitted (multipart: note + files[])
        public async Task<Response> Submit(Guid submissionId, string? note, List<IFormFile>? files)
        {
            var userId = GetUserIdOrThrow();

            await using var tx = await _ctx.Database.BeginTransactionAsync();
            try
            {
                var sub = await _ctx.Submissions.FirstOrDefaultAsync(s => s.Id == submissionId);
                if (sub == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
                if (!string.Equals(sub.Status, "Draft", StringComparison.OrdinalIgnoreCase))
                    return new Response { Code = ResponseCode.BadRequest, Message = $"Invalid state: {sub.Status}" };

                // add files
                if (files != null && files.Count > 0)
                {
                    foreach (var f in files)
                    {
                        var r = await AddSubmissionFile(submissionId, f, userId);
                        if (r.Code != ResponseCode.Success)
                        {
                            await tx.RollbackAsync();
                            return r;
                        }
                    }
                }

                var now = DateTime.UtcNow;
                sub.Status = "Submitted";
                sub.LastUpdated = now;

                _ctx.SubmissionEvents.Add(new SubmissionEvent
                {
                    Id = Guid.NewGuid(),
                    SubmissionId = submissionId,
                    Action = "Submit",
                    FromStatus = "Draft",
                    ToStatus = "Submitted",
                    ByUserId = userId,
                    ByRole = "Executor",
                    Note = note,
                    At = now
                });

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
                return new Response { Code = ResponseCode.Success, Message = "Submitted" };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new Response { Code = ResponseCode.InternalServerError, Message = ex.Message };
            }
        }

        // PASS: Submitted -> ForApproval  (nếu không chọn toUserId)
        // hoặc vẫn Submitted nhưng chuyển lượt reviewer (nếu có toUserId)
        public async Task<Response> Pass(Guid submissionId, string? note, Guid? toUserId)
        {
            var userId = GetUserIdOrThrow();
            var sub = await _ctx.Submissions.Include(s => s.Events)
                                            .FirstOrDefaultAsync(s => s.Id == submissionId);
            if (sub == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
            if (!string.Equals(sub.Status, "Submitted", StringComparison.OrdinalIgnoreCase))
                return new Response { Code = ResponseCode.BadRequest, Message = $"Invalid state: {sub.Status}" };

            var now = DateTime.UtcNow;

            if (toUserId.HasValue && toUserId.Value != Guid.Empty && toUserId != userId)
            {
                // vẫn Submitted, chuyển lượt reviewer
                sub.LastUpdated = now;
                _ctx.SubmissionEvents.Add(new SubmissionEvent
                {
                    Id = Guid.NewGuid(),
                    SubmissionId = submissionId,
                    Action = "PassToReviewer",
                    FromStatus = "Submitted",
                    ToStatus = "Submitted",
                    ByUserId = userId,
                    ByRole = "Reviewer",
                    Note = note,
                    At = now,
                    TargetUserId = toUserId
                });

                await _ctx.SaveChangesAsync();
                return new Response { Code = ResponseCode.Success, Message = "Submitted" };
            }

            // kết vòng thẩm định
            sub.Status = "ForApproval";
            sub.LastUpdated = now;
            _ctx.SubmissionEvents.Add(new SubmissionEvent
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Action = "Pass",
                FromStatus = "Submitted",
                ToStatus = "ForApproval",
                ByUserId = userId,
                ByRole = "Reviewer",
                Note = note,
                At = now
            });

            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "ForApproval" };
        }

        // BACK: Submitted -> NeedsChanges (tuỳ chọn chỉ định người sẽ nhận để xử tiếp/resubmit)
        public async Task<Response> RequestChanges(Guid submissionId, string? note, Guid? toUserId)
        {
            var userId = GetUserIdOrThrow();
            var sub = await _ctx.Submissions.Include(s => s.Events)
                                            .FirstOrDefaultAsync(s => s.Id == submissionId);
            if (sub == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
            if (!string.Equals(sub.Status, "Submitted", StringComparison.OrdinalIgnoreCase))
                return new Response { Code = ResponseCode.BadRequest, Message = $"Invalid state: {sub.Status}" };

            var now = DateTime.UtcNow;
            sub.Status = "NeedsChanges";
            sub.NextReviewerId = (toUserId.HasValue && toUserId.Value != Guid.Empty) ? toUserId : userId;
            sub.LastUpdated = now;

            _ctx.SubmissionEvents.Add(new SubmissionEvent
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Action = "RequestChanges",
                FromStatus = "Submitted",
                ToStatus = "NeedsChanges",
                ByUserId = userId,
                ByRole = "Reviewer",
                Note = note,
                At = now,
                TargetUserId = sub.NextReviewerId
            });

            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "NeedsChanges" };
        }

        // RESUBMIT: NeedsChanges -> Submitted
        public async Task<Response> Resubmit(Guid submissionId, string? note)
        {
            var userId = GetUserIdOrThrow();
            var sub = await _ctx.Submissions.Include(s => s.Events)
                                            .FirstOrDefaultAsync(s => s.Id == submissionId);
            if (sub == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
            if (!string.Equals(sub.Status, "NeedsChanges", StringComparison.OrdinalIgnoreCase))
                return new Response { Code = ResponseCode.BadRequest, Message = $"Invalid state: {sub.Status}" };

            var now = DateTime.UtcNow;
            sub.Status = "Submitted";
            sub.LastUpdated = now;

            _ctx.SubmissionEvents.Add(new SubmissionEvent
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Action = "Resubmit",
                FromStatus = "NeedsChanges",
                ToStatus = "Submitted",
                ByUserId = userId,
                ByRole = "Executor",
                Note = note,
                At = now,
                TargetUserId = sub.NextReviewerId
            });

            sub.NextReviewerId = null;
            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Submitted" };
        }

        // Approve / Reject giữ nguyên ForApproval
        public async Task<Response> Approve(Guid submissionId, string? note)
        {
            var r = await TransitionTracked(submissionId, "ForApproval", "Approved", "Approve", "Approver", note);
            if (r.Code != ResponseCode.Success) return r;

            var sub = await _ctx.Submissions.Include(s => s.Files).FirstOrDefaultAsync(s => s.Id == submissionId);
            if (sub != null) await PublishSubmissionFilesToPolicyDocumentsAsync(sub);
            return r;
        }

        public async Task<Response> Reject(Guid submissionId, string? note)
            => await TransitionTracked(submissionId, "ForApproval", "NeedsChanges", "Reject", "Approver", note);

        /* ======================= Files ======================= */

        public async Task<Response> ListFiles(Guid submissionId)
        {
            var files = await _ctx.SubmissionFiles.AsNoTracking()
                .Where(f => f.SubmissionId == submissionId)
                .OrderByDescending(f => f.UploadedAt)
                .Select(f => new
                {
                    f.Id,
                    f.FileName,
                    f.FileSize,
                    f.UploadedAt,
                    f.Category,
                    f.IsSelectedForPublish,
                    f.PublishedDocumentId,
                    DownloadUrl = _files.BuildDownloadUrl(f.StoragePath)
                })
                .ToListAsync();

            return new Response { Code = ResponseCode.Success, Data = files };
        }

        public async Task<Response> UploadFile(Guid submissionId, IFormFile file, Guid uploadedBy)
        {
            var r = await AddSubmissionFile(submissionId, file, uploadedBy);
            if (r.Code != ResponseCode.Success) return r;
            await _ctx.SaveChangesAsync();
            return r;
        }

        public async Task<Response> UpdateFile(Guid submissionId, Guid fileId, UpdateSubmissionFileDTO dto)
        {
            var f = await _ctx.SubmissionFiles.FirstOrDefaultAsync(x => x.Id == fileId && x.SubmissionId == submissionId);
            if (f == null) return new Response { Code = ResponseCode.NotFound, Message = "File not found" };

            if (dto.Category.HasValue) f.Category = dto.Category.Value;
            if (dto.IsSelectedForPublish.HasValue) f.IsSelectedForPublish = dto.IsSelectedForPublish.Value;

            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Updated" };
        }

        public async Task<Response> DeleteFile(Guid submissionId, Guid fileId)
        {
            var f = await _ctx.SubmissionFiles.FirstOrDefaultAsync(x => x.Id == fileId && x.SubmissionId == submissionId);
            if (f == null) return new Response { Code = ResponseCode.NotFound, Message = "File not found" };
            if (f.PublishedDocumentId.HasValue)
                return new Response { Code = ResponseCode.BadRequest, Message = "Cannot delete published file" };

            _ctx.SubmissionFiles.Remove(f);
            await _ctx.SaveChangesAsync();

            _files.Delete(f.StoragePath);
            return new Response { Code = ResponseCode.Success, Message = "Deleted" };
        }

        /* ======================= Query helpers ======================= */

        private async Task<Response> AddSubmissionFile(Guid submissionId, IFormFile file, Guid uploadedBy)
        {
            var subExists = await _ctx.Submissions.AsNoTracking().AnyAsync(s => s.Id == submissionId);
            if (!subExists) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };

            if (file == null || file.Length <= 0) return new Response { Code = ResponseCode.BadRequest, Message = "Empty file" };
            const long max = 25L * 1024 * 1024;
            if (file.Length > max) return new Response { Code = ResponseCode.BadRequest, Message = "File quá lớn (tối đa 25MB)." };

            var storedPath = await _files.SaveAsync(file, $"Uploads/Workflow/Submissions/{submissionId}/Files");

            _ctx.SubmissionFiles.Add(new SubmissionFile
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                FileName = file.FileName,
                StoragePath = storedPath,
                ContentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType,
                FileSize = file.Length,
                IsImage = (file.ContentType?.StartsWith("image/", StringComparison.OrdinalIgnoreCase) ?? false),
                UploadedAt = DateTime.UtcNow,
                UploadedBy = uploadedBy
            });

            return new Response { Code = ResponseCode.Success, Message = "Added" };
        }

        private async Task<Response> TransitionTracked(Guid submissionId, string from, string to, string action, string byRole, string? note = null)
        {
            var userId = GetUserIdOrThrow();
            var sub = await _ctx.Submissions.Include(s => s.Events).FirstOrDefaultAsync(s => s.Id == submissionId);
            if (sub == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
            if (!string.Equals(sub.Status, from, StringComparison.OrdinalIgnoreCase))
                return new Response { Code = ResponseCode.BadRequest, Message = $"Invalid state: {sub.Status}" };

            sub.Status = to;
            sub.LastUpdated = DateTime.UtcNow;

            _ctx.SubmissionEvents.Add(new SubmissionEvent
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Action = action,
                FromStatus = from,
                ToStatus = to,
                ByUserId = userId,
                ByRole = byRole,
                Note = note,
                At = DateTime.UtcNow
            });

            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = to };
        }

        private async Task PublishSubmissionFilesToPolicyDocumentsAsync(Submission sub)
        {
            var files = await _ctx.SubmissionFiles
                .Where(f => f.SubmissionId == sub.Id)
                .ToListAsync();

            foreach (var f in files)
            {
                if (f.PublishedDocumentId.HasValue) continue;

                var doc = new PolicyDocument
                {
                    Id = Guid.NewGuid(),
                    PolicyStepId = sub.PolicyStepId,
                    FileName = f.FileName,
                    FileSize = f.FileSize,
                    StoragePath = f.StoragePath,
                    UploadedAt = DateTime.UtcNow,
                    UploadedBy = f.UploadedBy,
                    Category = f.Category == SubmissionFileCategory.Approval
                                ? DocumentCategory.Approval
                                : DocumentCategory.General
                };

                _ctx.PolicyDocument.Add(doc);
                f.PublishedDocumentId = doc.Id;
            }

            await _ctx.SaveChangesAsync();
        }

        private Guid GetUserIdOrThrow()
        {
            var u = _http.HttpContext?.User;
            var idStr = u?.FindFirstValue(ClaimTypes.NameIdentifier)
                       ?? u?.FindFirstValue("sub")
                       ?? u?.FindFirstValue("oid")
                       ?? u?.FindFirstValue("uid");

            if (!Guid.TryParse(idStr, out var gid) || gid == Guid.Empty)
                throw new UnauthorizedAccessException("Invalid user id in token.");
            return gid;
        }

        /* ======================= Reads ======================= */

        public async Task<Response> GetSubmission(Guid id)
        {
            var data = await _ctx.Submissions
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.PolicyStepId,
                    s.Title,
                    s.Description,
                    s.Status,
                    s.CreatedAt,
                    s.LastUpdated,
                    s.CurrentReviewerId,
                    s.NextReviewerId,
                    Files = s.Files.OrderByDescending(f => f.UploadedAt)
                        .Select(f => new {
                            f.Id,
                            f.FileName,
                            f.FileSize,
                            f.UploadedAt,
                            f.Category,
                            f.IsSelectedForPublish,
                            DownloadUrl = _files.BuildDownloadUrl(f.StoragePath)
                        }).ToList(),
                    Events = s.Events.OrderByDescending(e => e.At)
                        .Select(e => new {
                            e.Id,
                            e.Action,
                            e.FromStatus,
                            e.ToStatus,
                            e.ByUserId,
                            e.ByRole,
                            e.Note,
                            e.At,
                            e.TargetUserId
                        }).ToList(),
                    Comments = s.Comments.OrderByDescending(c => c.CreatedAt)
                        .Select(c => new {
                            c.Id,
                            c.Content,
                            c.ByRole,
                            c.CreatedAt,
                            c.ByUserId,
                            ByUserName = _ctx.Users.Where(u => u.Id == c.ByUserId)
                                                   .Select(u => u.UserInformation.FullName).FirstOrDefault()
                        }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (data == null) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };
            return new Response { Code = ResponseCode.Success, Data = data };
        }

        public async Task<Response> ListComments(Guid submissionId)
        {
            var comments = await _ctx.SubmissionComments.AsNoTracking()
                .Where(c => c.SubmissionId == submissionId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return new Response { Code = ResponseCode.Success, Data = comments };
        }

        public async Task<Response> AddComment(Guid submissionId, string content, string byRole)
        {
            var subExists = await _ctx.Submissions.AsNoTracking().AnyAsync(s => s.Id == submissionId);
            if (!subExists) return new Response { Code = ResponseCode.NotFound, Message = "Submission not found" };

            var idStr = _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var uid = Guid.TryParse(idStr, out var g) ? g : Guid.Empty;

            _ctx.SubmissionComments.Add(new SubmissionComment
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Content = content,
                ByRole = byRole,
                CreatedAt = DateTime.UtcNow,
                ByUserId = uid
            });
            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Added" };
        }
    }
}

using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Emails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace DataAccess.Okrs
{
    public class OkrDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly EmailDAO _emailDAO;

        public OkrDAO(SEP490_G49Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> hubContext, EmailDAO emailDAO)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
            _emailDAO = emailDAO;
        }

        public async Task<Response> GetOkrsSlowProgress()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            DateTime currentDate = DateTime.Now;
            int currentQuarter = (currentMonth - 1) / 3 + 1;
            int startMonthOfQuarter = (currentQuarter - 1) * 3 + 1;
            DateTime startOfQuarter = new DateTime(currentYear, startMonthOfQuarter, 1);
            int daysSinceStartOfQuarter = (currentDate - startOfQuarter).Days + 1;
            DateTime endOfQuarter = startOfQuarter.AddMonths(3).AddDays(-1);
            int totalDaysInQuarter = (endOfQuarter - startOfQuarter).Days + 1;
            int progressPercentage = (int)((daysSinceStartOfQuarter * 100 / totalDaysInQuarter));

            List<OKR> okrsSlowProgresses = _context.OKRs.Where(x => x.Progress < progressPercentage).ToList();

            foreach (var okrslowProgress in okrsSlowProgresses)
            {
                User user = _context.Users.Include(x => x.UserInformation).FirstOrDefault(x => x.Id == okrslowProgress.Owner);
                if (user != null)
                {
                    var email = user.UserInformation.Email;
                    var subject = "OKR Status - Slow Progress Alert";

                    var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9;'>
                    <div style='background-color: #4CAF50; color: white; padding: 10px 0; text-align: center; font-size: 24px; font-weight: bold;'>
                        OKR Status Update
                    </div>
                    <div style='margin-top: 20px; line-height: 1.6;'>
                        <p>Dear {user.UserInformation.FullName},</p>
                        <p>We noticed that your current OKR progress is below the expected rate for this quarter.</p>
                        <p><strong>OKR Progress:</strong> {okrslowProgress.Progress}%</p>
                        <p>As of today, the quarter is <span style='color: #d9534f; font-weight: bold;'>{progressPercentage}%</span> complete.</p>
                        <p>We encourage you to review your goals and take any necessary actions to get back on track.</p>
                        <p>If you need any assistance or have any questions, please do not hesitate to reach out to your manager.</p>
                        <p>Best regards,</p>
                        <p>The OKR Management</p>
                    </div>
                    <div style='margin-top: 30px; font-size: 12px; color: #999; text-align: center;'>
                        &copy; {currentYear} SHRS Company. All rights reserved.
                    </div>
                </div>";
                    await _emailDAO.SendEmail(email, subject, body);
                }
            }

            return new Response { Code = ResponseCode.Success };
        }
        public async Task<PaginatedList<OKRDTO>> GetAllOkrs(
     int pageIndex = 1,
     int pageSize = 10,
     string? title = null,
     string? type = null,
     string? scope = null,
     string? status = null,
     string? cycle = null,
     Guid? departmentId = null)
        {
            var query = _context.OKRs.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(j => j.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(j => j.Type == type);
            }

            if (!string.IsNullOrEmpty(scope))
            {
                query = query.Where(j => j.Scope == scope);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(j => j.Status == status);
            }
            if (!string.IsNullOrEmpty(cycle))
            {
                query = query.Where(j => j.Cycle == cycle);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(j => j.DepartmentId == departmentId);
            }

            query = query.Where(j => j.ApproveStatus == "Approve"); // Only get approved OKRs

            var okrs = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var okrList = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OKRDTO
                {
                    Id = o.Id,
                    Title = o.Title,
                    Type = o.Type,
                    Scope = o.Scope,
                    Owner = _context.Users.FirstOrDefault(x => x.Id == o.Owner).UserInformation.FullName,
                    OwnerId = o.Owner,
                    DepartmentName = o.Department.Name,
                    Status = o.Status,
                    Cycle = o.Cycle,
                    Progress = o.Progress,
                    ParentAlignment = _context.OKRs.FirstOrDefault(x => x.Id == o.ParentId).Title,
                    ParentId = o.ParentId,
                    DepartmentId = o.DepartmentId
                })
                .ToListAsync(); ;

            return new PaginatedList<OKRDTO>(okrList, pageIndex, totalPages, count);
        }

        public async Task<PaginatedList<OKRRequestDTO>> GetOkrsRequests(
    int pageIndex = 1,
    int pageSize = 10,
    string? title = null,
    string? type = null,
    string? scope = null,
    string? approveStatus = null,
     string? cycle = null,
    Guid? departmentId = null)
        {
            var query = _context.OKRs.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(o => o.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(o => o.Type == type);
            }

            if (!string.IsNullOrEmpty(scope))
            {
                query = query.Where(o => o.Scope == scope);
            }

            if (!string.IsNullOrEmpty(approveStatus))
            {
                query = query.Where(o => o.ApproveStatus == approveStatus);
            }
            if (!string.IsNullOrEmpty(cycle))
            {
                query = query.Where(o => o.Cycle == cycle);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(o => o.DepartmentId == departmentId);
            }

            var count = await query.CountAsync();
            var okrs = await query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OKRRequestDTO
                {
                    Id = o.Id,
                    Title = o.Title,
                    Type = o.Type,
                    Scope = o.Scope,
                    Owner = _context.Users.FirstOrDefault(x => x.Id == o.Owner).UserInformation.FullName,
                    OwnerId = o.Owner,
                    DateCreated = o.DateCreated,
                    DepartmentName = o.Department.Name,
                    ApproveStatus = o.ApproveStatus,
                    Cycle = o.Cycle,
                    ParentAligment = _context.OKRs.FirstOrDefault(x => x.Id == o.ParentId).Title,
                    ParentId = o.ParentId,
                    DepartmentId = o.DepartmentId
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<OKRRequestDTO>(okrs, pageIndex, totalPages, count);
        }

        public async Task<OKRDetailsDTO> GetOkrById(Guid id)
        {
            var okr = await _context.OKRs
         .Include(x => x.Department)
         .FirstOrDefaultAsync(x => x.Id == id);


            var user = await _context.Users
                .Include(u => u.UserInformation)
                .FirstOrDefaultAsync(u => u.Id == okr.Owner);


            var parentAlignment = await _context.OKRs
                .FirstOrDefaultAsync(p => p.Id == okr.ParentId);
            var okrDetails = new OKRDetailsDTO
            {
                Id = okr.Id,
                Title = okr.Title,
                Content = okr.Content,
                Type = okr.Type,
                Scope = okr.Scope,
                Owner = user.UserInformation.FullName,
                Progress = okr.Progress,
                Achieved = okr.Achieved,
                TargerNumber = okr.TargerNumber,
                UnitOfTarget = okr.UnitOfTarget,
                Status = okr.Status,
                ApproveStatus = okr.ApproveStatus,
                Reason = okr.Reason,
                Cycle = okr.Cycle,
                ConfidenceLevel = okr.ConfidenceLevel,
                ActionPlan = okr.ActionPlan,
                ActionPlanDetails = okr.ActionPlanDetail,
                Result = okr.Result,
                DateCreated = okr.DateCreated,
                ParentAlignment = parentAlignment?.Title,
                DepartmentId = okr.DepartmentId,
                DepartmentName = okr.Department?.Name,
                ParentId = okr.ParentId,
                OwnerId = okr.Owner
            };
            return okrDetails;
        }

        public async Task<Response> CreateOkr(OKRCreateDTO okrDTO)
        {
            // Lấy userId từ JWT Token
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var department = await _context.Departments
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == okrDTO.DepartmentId);
            string? uniqueFileName = null;
            if (okrDTO.ActionPlan != null)
            {
                var allowedExtensions = new[] { ".pdf" };
                var fileExtension = Path.GetExtension(okrDTO.ActionPlan.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                }

                var allowedMimeTypes = new[] { "application/pdf" };
                if (!allowedMimeTypes.Contains(okrDTO.ActionPlan.ContentType))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Okrs", okrDTO.Title);
                Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = okrDTO.ActionPlan.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await okrDTO.ActionPlan.CopyToAsync(fileStream);
                }
            }
            //var user = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            var newProgress = (int)Math.Round((double)okrDTO.Achieved / okrDTO.TargetNumber * 100);
            var newOkr = new OKR
            {
                Id = Guid.NewGuid(),
                ParentId = okrDTO.ParentId,
                Title = okrDTO.Title,
                Content = okrDTO.Content,
                Type = okrDTO.Type,
                DepartmentName = department.Name,
                TargetProgress = 100,
                UnitOfTarget = okrDTO.UnitOfTarget,
                TargerNumber = okrDTO.TargetNumber,
                Achieved = okrDTO.Achieved,
                Progress = newProgress,
                Status = GetStatusBasedOnProgress(newProgress),
                ApproveStatus = "Pending",
                Reason = "",
                Cycle = okrDTO.Cycle,
                ConfidenceLevel = okrDTO.ConfidenceLevel,
                Result = okrDTO.Result,
                DateCreated = DateTime.UtcNow.AddHours(7),
                DepartmentId = okrDTO.DepartmentId,
                Department = department,
                Owner = Guid.Parse(userId),
                Scope = okrDTO.Scope,
                ActionPlanDetail = uniqueFileName != null ? $"/Uploads/Okrs/{okrDTO.Title}/{uniqueFileName}" : null,
                ActionPlan = okrDTO.ActionPlan?.FileName
            };

            // Thêm OKR vào DbContext và lưu vào cơ sở dữ liệu
            _context.OKRs.Add(newOkr);
            //await _context.SaveChangesAsync();
            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = newOkr.Owner,
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = newOkr.Progress,
                NewProgress = newOkr.Progress,
                OldAchieved = newOkr.Achieved,
                NewAchieved = newOkr.Achieved,
                UnitOfTarget = okrDTO.UnitOfTarget,
                OldStatus = newOkr.Status,
                NewStatus = GetStatusBasedOnProgress(newOkr.Progress),
                Type = "logging",
                OkrId = newOkr.Id,
                Description = ""
            };

            _context.okrHistories.Add(okrH);
            await _context.SaveChangesAsync();

            if (okrDTO.ParentId.HasValue)
            {
                await UpdateParentOkrProgress(okrDTO.ParentId);
            }
            return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
        }

        public async Task<Response> UpdateProgressOkr(Guid okrId, int achieved)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tìm kiếm objective hiện tại
            var okr = await _context.OKRs
                .Include(o => o.Department)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            var okrHi = await _context.okrHistories
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync(x => x.OkrId == okrId);

            if (okr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }
            if (achieved > okr.TargerNumber)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Achieved is not larger than TargetNumber" };
            }

            okr.Progress = (int)Math.Round((double)achieved / okr.TargerNumber * 100);
            okr.Achieved = achieved;
            okr.Status = GetStatusBasedOnProgress(okr.Progress);

            _context.OKRs.Update(okr);
            await _context.SaveChangesAsync();

            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = okrHi.NewProgress,
                NewProgress = okr.Progress,
                OldAchieved = okrHi.NewAchieved,
                NewAchieved = achieved,
                UnitOfTarget = okrHi.UnitOfTarget,
                OldStatus = okrHi.NewStatus,
                Type = "logging",
                NewStatus = okr.Status,
                Description = "",
                OkrId = okr.Id
            };

            _context.okrHistories.Add(okrH);
            await _context.SaveChangesAsync();

            await UpdateParentOkrProgress(okr.ParentId);

            return new Response { Code = ResponseCode.Success, Message = "Update successfully!" };
        }
        public string GetStatusBasedOnProgress(int progress)
        {
            if (progress == 0)
                return "Not Started";
            else if (progress > 0 && progress < 100)
                return "Processing";
            else if (progress >= 100)
                return "Done";
            else
                return "Pending";
        }

        private async Task UpdateParentOkrProgress(Guid? parentId)
        {
            if (!parentId.HasValue)
            {
                return;
            }

            var parentOkr = await _context.OKRs
                .Include(o => o.Department)
                .FirstOrDefaultAsync(o => o.Id == parentId.Value);

            if (parentOkr == null)
            {
                return;
            }

            // Lấy tất cả các OKR con của OKR cha
            var childOkrs = await _context.OKRs.Where(o => o.ParentId == parentId && (o.Status == "Processing" || o.Status == "Done") && o.ApproveStatus == "Approve").ToListAsync();

            if (childOkrs.Count == 0)
            {
                return; // Nếu không có OKR con nào, không cần cập nhật gì cả
            }

            // Lưu lại trạng thái, tiến độ và Achieved cũ của OKR cha
            var oldProgress = parentOkr.Progress;
            var oldStatus = parentOkr.Status;
            var oldAchieved = parentOkr.Achieved;

            // Tính toán tổng và trung bình Achieved của tất cả các OKR con
            var averageAchieved = (int)Math.Round(childOkrs.Average(o => o.Achieved));

            // Cập nhật giá trị Achieved mới cho OKR cha
            parentOkr.Achieved = averageAchieved;

            // Tính toán tiến độ của OKR cha dựa trên tiến độ trung bình của tất cả các OKR con
            parentOkr.Progress = (int)Math.Round(childOkrs.Average(o => o.Progress));

            // Cập nhật trạng thái của OKR cha
            parentOkr.Status = GetStatusBasedOnProgress(parentOkr.Progress);

            _context.OKRs.Update(parentOkr);
            await _context.SaveChangesAsync();

            // Tạo một bản ghi mới trong okrHistories cho OKR cha
            var parentOkrHistory = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = oldProgress,
                NewProgress = parentOkr.Progress,
                OldAchieved = oldAchieved,
                NewAchieved = averageAchieved,
                UnitOfTarget = parentOkr.UnitOfTarget, 
                OldStatus = oldStatus,
                NewStatus = parentOkr.Status,
                Type = "logging",
                Description = "",
                OkrId = parentOkr.Id
            };

            _context.okrHistories.Add(parentOkrHistory);
            await _context.SaveChangesAsync();

            // Cập nhật tiếp OKR cha của OKR cha nếu có
            await UpdateParentOkrProgress(parentOkr.ParentId);
        }



        public async Task<Response> UpdateOwnerOkr(Guid okrId, Guid Owner)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tìm kiếm objective hiện tại
            var okr = await _context.OKRs
                .Include(o => o.Department)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            var okrHi = await _context.okrHistories
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync(x => x.OkrId == okrId);

            if (okr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }


            okr.Owner = Owner;
            var user = await _context.Users
                .Include(u => u.UserInformation)
                .FirstOrDefaultAsync(u => u.Id == Owner);
            _context.OKRs.Update(okr);
            await _context.SaveChangesAsync();

            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = okrHi.NewProgress,
                NewProgress = okr.Progress,
                OldStatus = okrHi.NewStatus,
                OldAchieved = okrHi.OldAchieved,
                NewAchieved = okrHi.NewAchieved,
                UnitOfTarget = okrHi.UnitOfTarget,
                Type = "logging",
                NewStatus = okr.Status,
                Description = "The new Owner is " + user.UserInformation.FullName,
                OkrId = okr.Id
            };

            _context.okrHistories.Add(okrH);
            await _context.SaveChangesAsync();
            var message1 = $"The OKR have title '{okr.Title}' is changed for you";
            var redirectUrl = "";
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = message1,
                CreatedAt = DateTime.UtcNow.AddHours(7),
                IsRead = false,
                UserId = Owner,
                RedirectUrl = redirectUrl,
                User = user
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", Owner, new Notification
            {
                CreatedAt = DateTime.Now.AddHours(7),
                Message = message1,
            });

            return new Response { Code = ResponseCode.Success, Message = "Update successfully!" };
        }

        public async Task<Response> UpdateApproveStatus(Guid okrId, ApproveStatusUpdateDTO dto)
        {
            var oKR = await _context.OKRs.FindAsync(okrId);
            var user = _context.Users.FirstOrDefault(x => x.Id == oKR.Owner);
            if (oKR == null)
            {
                return new Response { Message = "Objective not found." };
            }

            oKR.ApproveStatus = dto.ApproveStatus;

            if (dto.ApproveStatus == "Reject")
            {
                if (string.IsNullOrEmpty(dto.Reason))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Reason is required when rejecting an OKR." };
                }
                oKR.Reason = dto.Reason;
            }
            else if (dto.ApproveStatus == "Approve")
            {
                oKR.Reason = dto.Reason;
            }

            _context.OKRs.Update(oKR);
            await _context.SaveChangesAsync();

            var notificationMessage = dto.ApproveStatus == "Approve" ? $"OKR '{oKR.Title}' approved" : $"OKR '{oKR.Title}' rejected";
            var redirectUrl = "";
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = notificationMessage,
                CreatedAt = DateTime.UtcNow.AddHours(7),
                IsRead = false,
                UserId = oKR.Owner,
                RedirectUrl = redirectUrl,
                User = user
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send real-time notification via SignalR (optional)
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", oKR.Owner, new Notification
            {
                Message = notificationMessage,
                RedirectUrl = redirectUrl
            });

            return new Response { Message = "Update successfully!" };
        }
        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            var query = _context.OKRs.AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(o => o.DepartmentId == departmentId);
            }

            var notStartedCount = query.Count(o => o.Status == "Not Started");
            var processingCount = query.Count(o => o.Status == "Processing");
            var doneCount = query.Count(o => o.Status == "Done");

            var result = new Dictionary<string, int>
        {
            { "NotStarted", notStartedCount },
            { "Processing", processingCount },
            { "Done", doneCount }
        };

            return result;
        }

        public IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId)
        {
            var query = _context.OKRs.AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(o => o.DepartmentId == departmentId);
            }

            var pendingCount = query.Count(o => o.ApproveStatus == "Pending");
            var approveCount = query.Count(o => o.ApproveStatus == "Approve");
            var rejectCount = query.Count(o => o.ApproveStatus == "Reject");

            var result = new Dictionary<string, int>
        {
            { "Pending", pendingCount },
            { "Approve", approveCount },
            { "Reject", rejectCount }
        };

            return result;
        }
        public async Task<Response> UpdateOkrRequest(Guid okrId, OKREditDTO okrEditDTO)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tìm kiếm objective hiện tại
            var existingOkr = await _context.OKRs
                .Include(o => o.Department)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            if (existingOkr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }
            if (existingOkr.ApproveStatus == "Reject" || existingOkr.ApproveStatus == "Pending")
            {
                string? uniqueFileName = null;
                if (okrEditDTO.ActionPlan != null)
                {
                    var allowedExtensions = new[] { ".pdf" };
                    var fileExtension = Path.GetExtension(okrEditDTO.ActionPlan.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                    }

                    var allowedMimeTypes = new[] { "application/pdf" };
                    if (!allowedMimeTypes.Contains(okrEditDTO.ActionPlan.ContentType))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                    }

                    var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Okrs", existingOkr.Title);
                    Directory.CreateDirectory(uploadsFolder);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + okrEditDTO.ActionPlan.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await okrEditDTO.ActionPlan.CopyToAsync(fileStream);
                    }
                }

                existingOkr.Title = okrEditDTO.Title;
                existingOkr.Content = okrEditDTO.Content;
                existingOkr.Type = okrEditDTO.Type;
                existingOkr.Scope = okrEditDTO.Scope;
                existingOkr.TargerNumber = okrEditDTO.TargetNumber;
                existingOkr.Achieved = okrEditDTO.Achieved;
                existingOkr.UnitOfTarget = okrEditDTO.UnitOfTarget;
                existingOkr.Cycle = okrEditDTO.Cycle;
                existingOkr.ConfidenceLevel = okrEditDTO.ConfidenceLevel;
                existingOkr.Result = okrEditDTO.Result;
                existingOkr.DepartmentId = okrEditDTO.DepartmentId;
                existingOkr.ParentId = okrEditDTO.ParentId;
                existingOkr.ApproveStatus = "Pending";
                existingOkr.Progress = (int)Math.Round((double)okrEditDTO.Achieved / okrEditDTO.TargetNumber * 100);

                existingOkr.ActionPlanDetail = uniqueFileName != null ? $"/Uploads/Okrs/{existingOkr.Title}/{uniqueFileName}" : existingOkr.ActionPlanDetail;
                existingOkr.ActionPlan = okrEditDTO.ActionPlan?.FileName ?? existingOkr.ActionPlan;

                _context.OKRs.Update(existingOkr);
                await _context.SaveChangesAsync();

            }
            return new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };
        }

    }
}

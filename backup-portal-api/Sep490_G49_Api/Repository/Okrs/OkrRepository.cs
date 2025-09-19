using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Emails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Repository.Objectives
{
    public class OkrRepository : IOkrRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly EmailDAO _emailDAO;

        public OkrRepository(SEP490_G49Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> hubContext, EmailDAO emailDAO)
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
                User user = _context.Users.Include(x => x.UserInformation).FirstOrDefault(x => x.Id == okrslowProgress.OkrUsers.First().UserId);
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

        public async Task<PaginatedList<OKRDTO>> GetOkrsByDepartmentId(
            int pageIndex = 1,
            int pageSize = 10,
            string? title = null,
            string? type = null,
            string? scope = null,
            string? status = null,
            string? cycle = null,
            Guid? departmentId = null)
        {
            var query = _context.OKRs
                .Include(o => o.OkrUsers)
                    .ThenInclude(ou => ou.User)
                    .ThenInclude(u => u.UserInformation)
                .Include(o => o.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(j => j.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(j => j.Type == type);

            if (!string.IsNullOrWhiteSpace(scope))
                query = query.Where(j => j.Scope == scope);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(j => j.Status == status);

            if (!string.IsNullOrWhiteSpace(cycle))
                query = query.Where(j => j.Cycle == cycle);

            // LỌC PHÒNG BAN: chỉ áp dụng khi departmentId hợp lệ,
            // và khi áp dụng thì CHỈ lấy OKR có quan hệ OkrDepartments khớp departmentId
            if (departmentId.HasValue && departmentId.Value != Guid.Empty)
            {
                query = query.Where(j => j.OkrDepartments.Any(od => od.DepartmentId == departmentId.Value));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var okrs = await query
                .OrderByDescending(o => o.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var okrList = okrs.Select(o =>
            {
                // ==== Owner (danh sách)
                var ownerNames = o.OkrUsers
                    .Select(ou => ou.User?.UserInformation?.FullName ?? string.Empty)
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

                var ownerIds = o.OkrUsers
                    .Select(ou => ou.UserId)
                    .Where(id => id != Guid.Empty)
                    .ToList();

                // ManagerNames: ví dụ lấy tất cả trừ owner đầu tiên (nếu bạn đang dùng quy ước này)
                var managerNames = ownerNames.Skip(1).ToList();

                // ==== DepartmentName: chỉ từ quan hệ (không fallback tên phẳng)
                var deptNames = o.OkrDepartments
                    .Select(od => od.Department?.Name ?? string.Empty)
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

                return new OKRDTO
                {
                    Id = o.Id,
                    Title = o.Title,
                    Content = o.Content,
                    Type = o.Type,
                    Scope = o.Scope,

                    OwnerNames = ownerNames,   // Danh sách tên owner
                    OwnerId = ownerIds,        // Danh sách Guid owner
                    ManagerNames = managerNames,

                    DepartmentName = deptNames,

                    Progress = o.Progress,
                    TargetProgress = o.TargetProgress,
                    TargetNumber = o.TargetNumber,
                    Achieved = o.Achieved,
                    UnitOfTarget = o.UnitOfTarget,
                    Status = o.Status,
                    ApproveStatus = o.ApproveStatus,
                    Reason = o.Reason,
                    Cycle = o.Cycle,
                    ConfidenceLevel = o.ConfidenceLevel,

                    ActionPlan = o.ActionPlan,
                    ActionPlanDetail = o.ActionPlanDetail,
                    Result = o.Result,

                    DateCreated = o.DateCreated,
                    DueDate = o.DueDate,
                    Priority = o.Priority,
                    Company = o.Company,
                    LastUpdated = o.LastUpdated,
                    Note = o.Note,

                    ParentAlignment = o.ParentId.HasValue
                        ? _context.OKRs.Where(x => x.Id == o.ParentId).Select(x => x.Title).FirstOrDefault()
                        : null,
                    ParentId = o.ParentId
                };
            }).ToList();

            return new PaginatedList<OKRDTO>(okrList, pageIndex, totalPages, count);
        }



        public async Task<OKRDetailsDTO> GetOkrById(Guid id)
        {
            var okr = await _context.OKRs
                .Include(x => x.OkrUsers)
                    .ThenInclude(ou => ou.User)
                    .ThenInclude(u => u.UserInformation)
                .Include(x => x.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            var ownerUser = okr.OkrUsers.FirstOrDefault()?.User;
            var owner = ownerUser != null && ownerUser.UserInformation != null ? ownerUser.UserInformation.FullName : "";

            string parentAlignment = null;
            if (okr.ParentId.HasValue)
            {
                var parentOkr = await _context.OKRs
                    .Where(p => p.Id == okr.ParentId.Value)
                    .Select(p => p.Title)
                    .FirstOrDefaultAsync();
                parentAlignment = parentOkr;
            }
            var okrDetails = new OKRDetailsDTO
            {
                Id = okr.Id,
                Title = okr.Title,
                Content = okr.Content,
                Type = okr.Type,
                Scope = okr.Scope,
                Owner = owner,
                Progress = okr.Progress,
                Achieved = okr.Achieved,
                TargerNumber = okr.TargetNumber,
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
                ParentAlignment = parentAlignment,
                DepartmentId = okr.OkrDepartments.FirstOrDefault()?.DepartmentId,
                DepartmentName = okr.OkrDepartments.FirstOrDefault()?.Department?.Name,
                ParentId = okr.ParentId,
                OwnerId = ownerUser?.Id
            };
            return okrDetails;
        }

        public async Task<Response> CreateOkr(OKRCreateDTO okrDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var department = await _context.Departments
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == okrDTO.DepartmentId);

            if (department == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Department not found." };
            }

            string? uniqueFileName = null;
            if (okrDTO.ActionPlanFile != null)
            {
                var allowedExtensions = new[] { ".pdf" };
                var fileExtension = Path.GetExtension(okrDTO.ActionPlanFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                }

                var allowedMimeTypes = new[] { "application/pdf" };
                if (!allowedMimeTypes.Contains(okrDTO.ActionPlanFile.ContentType))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Okrs", okrDTO.Title ?? Guid.NewGuid().ToString());
                Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + "_" + okrDTO.ActionPlanFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await okrDTO.ActionPlanFile.CopyToAsync(fileStream);
                }
            }

            var newProgress = okrDTO.Achieved.HasValue && okrDTO.TargetNumber.HasValue
                ? (int)Math.Round((double)okrDTO.Achieved.Value / okrDTO.TargetNumber.Value * 100)
                : 0;
            var status = okrDTO.Status ?? GetStatusBasedOnProgress(newProgress);

            var newOkr = new OKR
            {
                Id = Guid.NewGuid(),
                ParentId = okrDTO.ParentId,
                Title = okrDTO.Title ?? "Untitled",
                Content = okrDTO.Content,
                Type = okrDTO.Type,
                DepartmentName = department.Name,
                TargetProgress = okrDTO.TargetProgress ?? 100,
                UnitOfTarget = okrDTO.UnitOfTarget,
                TargetNumber = okrDTO.TargetNumber ?? 0,
                Achieved = okrDTO.Achieved ?? 0,
                Progress = newProgress,
                Status = status,
                ApproveStatus = okrDTO.ApproveStatus ?? "Pending",
                Reason = okrDTO.Reason,
                Cycle = okrDTO.Cycle,
                ConfidenceLevel = okrDTO.ConfidenceLevel,
                Result = okrDTO.Result,
                DateCreated = okrDTO.DateCreated ?? DateTime.UtcNow.AddHours(7),
                DueDate = okrDTO.DueDate,
                Priority = okrDTO.Priority,
                Company = okrDTO.Company,
                LastUpdated = okrDTO.LastUpdated ?? DateTime.UtcNow.AddHours(7),
                Note = okrDTO.Note,
                Scope = okrDTO.Scope,
                ActionPlanDetail = uniqueFileName != null ? $"/Uploads/Okrs/{(okrDTO.Title ?? Guid.NewGuid().ToString())}/{uniqueFileName}" : null,
                ActionPlan = okrDTO.ActionPlan
            };

            _context.OKRs.Add(newOkr);
            await _context.SaveChangesAsync();

            var okrDepartment = new OkrDepartment
            {
                OkrId = newOkr.Id,
                DepartmentId = okrDTO.DepartmentId
            };
            _context.OkrDepartments.Add(okrDepartment);

            var defaultOkrUser = new OkrUser
            {
                OkrId = newOkr.Id,
                UserId = Guid.Parse(userId)
            };
            _context.OkrUsers.Add(defaultOkrUser);

            if (okrDTO.OwnerIds != null && okrDTO.OwnerIds.Any())
            {
                foreach (var oid in okrDTO.OwnerIds)
                {
                    if (oid != Guid.Parse(userId))
                    {
                        var additionalOkrUser = new OkrUser
                        {
                            OkrId = newOkr.Id,
                            UserId = oid
                        };
                        _context.OkrUsers.Add(additionalOkrUser);
                    }
                }
            }

            await _context.SaveChangesAsync();

            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = newOkr.Progress,
                NewProgress = newOkr.Progress,
                OldAchieved = newOkr.Achieved,
                NewAchieved = newOkr.Achieved,
                UnitOfTarget = okrDTO.UnitOfTarget,
                OldStatus = newOkr.Status,
                NewStatus = status,
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

            var okr = await _context.OKRs
                .Include(o => o.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            var okrHi = await _context.okrHistories
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync(x => x.OkrId == okrId);

            if (okr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }
            if (achieved > okr.TargetNumber)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Achieved is not larger than TargetNumber" };
            }

            okr.Progress = (int)Math.Round((double)achieved / okr.TargetNumber * 100);
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
                OldAchieved = okrHi.OldAchieved,
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
                .Include(o => o.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .FirstOrDefaultAsync(o => o.Id == parentId.Value);

            if (parentOkr == null)
            {
                return;
            }

            var childOkrs = await _context.OKRs.Where(o => o.ParentId == parentId && (o.Status == "Processing" || o.Status == "Done") && o.ApproveStatus == "Approve").ToListAsync();

            if (childOkrs.Count == 0)
            {
                return;
            }

            var oldProgress = parentOkr.Progress;
            var oldStatus = parentOkr.Status;
            var oldAchieved = parentOkr.Achieved;

            var averageAchieved = (int)Math.Round(childOkrs.Average(o => o.Achieved));

            parentOkr.Achieved = averageAchieved;
            parentOkr.Progress = (int)Math.Round(childOkrs.Average(o => o.Progress));
            parentOkr.Status = GetStatusBasedOnProgress(parentOkr.Progress);

            _context.OKRs.Update(parentOkr);
            await _context.SaveChangesAsync();

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

            await UpdateParentOkrProgress(parentOkr.ParentId);
        }

        public async Task<Response> UpdateOwnerOkr(Guid okrId, Guid? ownerId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var okr = await _context.OKRs
                .Include(o => o.OkrUsers)
                    .ThenInclude(ou => ou.User)
                    .ThenInclude(u => u.UserInformation)
                .Include(o => o.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            var okrHi = await _context.okrHistories
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync(x => x.OkrId == okrId);

            if (okr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }

            if (!ownerId.HasValue)
            {
                return new Response { Code = ResponseCode.Success, Message = "No changes made to owners." };
            }

            var newOwner = await _context.Users
                .Include(u => u.UserInformation)
                .FirstOrDefaultAsync(u => u.Id == ownerId.Value);

            if (newOwner == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "User not found." };
            }

            if (!okr.OkrUsers.Any(ou => ou.UserId == ownerId.Value))
            {
                var newOkrUser = new OkrUser
                {
                    OkrId = okrId,
                    UserId = ownerId.Value
                };
                _context.OkrUsers.Add(newOkrUser);
                await _context.SaveChangesAsync();
            }

            _context.OKRs.Update(okr);
            await _context.SaveChangesAsync();

            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldProgress = okrHi?.NewProgress ?? okr.Progress,
                NewProgress = okr.Progress,
                OldStatus = okrHi?.NewStatus ?? okr.Status,
                OldAchieved = okrHi?.OldAchieved ?? okr.Achieved,
                NewAchieved = okr.Achieved,
                UnitOfTarget = okrHi?.UnitOfTarget ?? okr.UnitOfTarget,
                Type = "logging",
                NewStatus = okr.Status,
                Description = $"A new Owner '{newOwner.UserInformation.FullName}' has been added",
                OkrId = okrId
            };

            _context.okrHistories.Add(okrH);
            await _context.SaveChangesAsync();

            var message1 = $"The OKR with title '{okr.Title}' is changed and assigned to you";
            var redirectUrl = "";
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = message1,
                CreatedAt = DateTime.UtcNow.AddHours(7),
                IsRead = false,
                UserId = ownerId.Value,
                RedirectUrl = redirectUrl,
                User = newOwner
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", ownerId.Value, new Notification
            {
                CreatedAt = DateTime.UtcNow.AddHours(7),
                Message = message1,
            });

            return new Response { Code = ResponseCode.Success, Message = "Update successfully!" };
        }

        public async Task<Response> UpdateApproveStatus(Guid okrId, ApproveStatusUpdateDTO dto)
        {
            var okr = await _context.OKRs
                .Include(o => o.OkrUsers)
                    .ThenInclude(ou => ou.User)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            if (okr == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };
            }

            okr.ApproveStatus = dto.ApproveStatus;

            if (dto.ApproveStatus == "Reject")
            {
                if (string.IsNullOrEmpty(dto.Reason))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Reason is required when rejecting an OKR." };
                }
                okr.Reason = dto.Reason;
            }
            else if (dto.ApproveStatus == "Approve")
            {
                okr.Reason = dto.Reason ?? string.Empty;
            }

            _context.OKRs.Update(okr);
            await _context.SaveChangesAsync();

            var notificationMessage = dto.ApproveStatus == "Approve" ? $"OKR '{okr.Title}' approved" : $"OKR '{okr.Title}' rejected";
            var redirectUrl = "";

            var ownerUsers = okr.OkrUsers.Select(ou => ou.User).Where(u => u != null).ToList();
            if (ownerUsers.Any())
            {
                var notifications = ownerUsers.Select(user => new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = notificationMessage,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsRead = false,
                    UserId = user.Id,
                    RedirectUrl = redirectUrl,
                    User = user
                }).ToList();

                _context.Notifications.AddRange(notifications);
                await _context.SaveChangesAsync();

                foreach (var user in ownerUsers)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", user.Id, new Notification
                    {
                        Message = notificationMessage,
                        RedirectUrl = redirectUrl
                    });
                }
            }

            return new Response { Code = ResponseCode.Success, Message = "Update successfully!" };
        }

        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            var query = _context.OKRs.AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(j => j.OkrDepartments.Any(od => od.DepartmentId == departmentId));
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
                query = query.Where(j => j.OkrDepartments.Any(od => od.DepartmentId == departmentId));
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

            var existingOkr = await _context.OKRs
                .Include(o => o.OkrDepartments)
                    .ThenInclude(od => od.Department)
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
                existingOkr.TargetNumber = okrEditDTO.TargetNumber;
                existingOkr.Achieved = okrEditDTO.Achieved;
                existingOkr.UnitOfTarget = okrEditDTO.UnitOfTarget;
                existingOkr.Cycle = okrEditDTO.Cycle;
                existingOkr.ConfidenceLevel = okrEditDTO.ConfidenceLevel;
                existingOkr.Result = okrEditDTO.Result;
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

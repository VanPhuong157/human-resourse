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
            string? cycle = null
            )
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
            //if (departmentId.HasValue && departmentId.Value != Guid.Empty)
            //{
            //    query = query.Where(j => j.OkrDepartments.Any(od => od.DepartmentId == departmentId.Value));
            //}

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var okrs = await query
                .OrderByDescending(o => o.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            var okrList = okrs.Select(MapToOkrDto).ToList();
            return new PaginatedList<OKRDTO>(okrList, pageIndex, totalPages, count);
        }



        public async Task<OKRDTO?> GetOkrById(Guid id)
        {
            var okr = await _context.OKRs
                .Include(x => x.OkrUsers)
                    .ThenInclude(ou => ou.User)
                    .ThenInclude(u => u.UserInformation)
                .Include(x => x.OkrDepartments)
                    .ThenInclude(od => od.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (okr == null) return null;
            return MapToOkrDto(okr);
        }


        public async Task<Response> CreateOkr(OKRCreateDTO dto)
        {
            var userIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr))
                return new Response { Code = ResponseCode.InternalServerError, Message = "Unauthenticated." };
            var currentUserId = Guid.Parse(userIdStr);

            // ✅ validate DepartmentIds
            if (dto.DepartmentIds == null || dto.DepartmentIds.Count == 0)
                return new Response { Code = ResponseCode.BadRequest, Message = "DepartmentIds is required." };

            var depts = await _context.Departments
                .Where(d => dto.DepartmentIds.Contains(d.Id))
                .ToListAsync();
            if (depts.Count != dto.DepartmentIds.Distinct().Count())
                return new Response { Code = ResponseCode.BadRequest, Message = "Some DepartmentIds are invalid." };

            // ✅ tính progress + status
            int target = dto.TargetNumber ?? 0;
            int achieved = dto.Achieved ?? 0;
            int newProgress = target > 0 ? (int)Math.Round((double)achieved / target * 100) : 0;
            var status = GetStatusBasedOnProgress(newProgress);

            // ✅ cycle theo quý hiện tại (giờ VN)
            var nowLocal = DateTime.UtcNow.AddHours(7);
            var cycle = ComputeCurrentQuarterCycle(nowLocal);

            var newOkr = new OKR
            {
                Id = Guid.NewGuid(),
                ParentId = dto.ParentId,

                Title = dto.Title ?? "Untitled",
                Content = dto.Content,
                Type = dto.Type,
                Scope = dto.Scope,

                // DepartmentName: để null/không set; list phòng ban sẽ lấy từ OkrDepartments
                TargetProgress = dto.TargetProgress ?? 100,
                UnitOfTarget = dto.UnitOfTarget,
                TargetNumber = target,
                Achieved = achieved,
                Progress = newProgress,

                Status = "To Do",
                ApproveStatus = dto.ApproveStatus ?? "Approve",
                Reason = null,

                Cycle = cycle,
                ConfidenceLevel = dto.ConfidenceLevel,
                Result = null,

                DateCreated = dto.DateCreated ?? nowLocal,
                DueDate = dto.DueDate,
                Company = dto.Company,
                LastUpdated = dto.LastUpdated ?? nowLocal,
                Note = dto.Note,

                ActionPlan = null,
                ActionPlanDetail = null
            };

            _context.OKRs.Add(newOkr);
            await _context.SaveChangesAsync();

            // ✅ map nhiều phòng ban
            var okrDepts = dto.DepartmentIds
                .Distinct()
                .Select(did => new OkrDepartment { OkrId = newOkr.Id, DepartmentId = did });
            _context.OkrDepartments.AddRange(okrDepts);

            // ✅ OkrUsers (role chuẩn: Creator/Owner/Manager)
            var okrUsers = new List<OkrUser>
    {
        new OkrUser { OkrId = newOkr.Id, UserId = currentUserId, Role = OkrRoles.Creator }
    };

            if (dto.OwnerIds != null)
            {
                okrUsers.AddRange(
                    dto.OwnerIds
                       .Where(id => id != currentUserId) // tránh trùng creator
                       .Distinct()
                       .Select(id => new OkrUser { OkrId = newOkr.Id, UserId = id, Role = OkrRoles.Owner })
                );
            }

            if (dto.ManagerIds != null)
            {
                okrUsers.AddRange(
                    dto.ManagerIds
                       .Where(id => id != currentUserId) // có thể cho phép creator đồng thời là manager nếu bạn muốn
                       .Distinct()
                       .Select(id => new OkrUser { OkrId = newOkr.Id, UserId = id, Role = OkrRoles.Manager })
                );
            }

            _context.OkrUsers.AddRange(okrUsers);
            await _context.SaveChangesAsync();

            // ✅ lịch sử khởi tạo
            var okrH = new OkrHistory
            {
                Id = Guid.NewGuid(),
                UserId = currentUserId,
                DateCreated = nowLocal,
                OldProgress = newOkr.Progress,
                NewProgress = newOkr.Progress,
                OldAchieved = newOkr.Achieved,
                NewAchieved = newOkr.Achieved,
                UnitOfTarget = newOkr.UnitOfTarget,
                OldStatus = newOkr.Status,
                NewStatus = newOkr.Status,
                Type = "logging",
                OkrId = newOkr.Id,
                Description = ""
            };
            _context.okrHistories.Add(okrH);
            await _context.SaveChangesAsync();

            if (dto.ParentId.HasValue)
                await UpdateParentOkrProgress(dto.ParentId);

            return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
        }

        private static string ComputeCurrentQuarterCycle(DateTime nowLocal)
        {
            int q = ((nowLocal.Month - 1) / 3) + 1;
            return $"Q{q}/{nowLocal.Year}";
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

        private void ReplaceUsersForRole(OKR okr, string role, IEnumerable<Guid>? newIds)
        {
            if (newIds is null) return;

            var newSet = new HashSet<Guid>(newIds.Where(id => id != Guid.Empty));

            // ✅ lọc tất cả user hiện tại trong role này
            var current = okr.OkrUsers.Where(x => x.Role == role).ToList();
            var currentSet = new HashSet<Guid>(current.Select(x => x.UserId));

            // ✅ xoá những người không còn trong danh sách mới
            var toRemove = current.Where(x => !newSet.Contains(x.UserId)).ToList();
            if (toRemove.Count > 0)
                _context.OkrUsers.RemoveRange(toRemove);

            // ✅ thêm người mới: chỉ người chưa có role này (dù có vai trò khác vẫn được thêm)
            var toAdd = newSet.Except(currentSet);
            foreach (var uid in toAdd)
            {
                okr.OkrUsers.Add(new OkrUser
                {
                    OkrId = okr.Id,
                    UserId = uid,
                    Role = role
                });
            }
        }

        public async Task UpdateDepartmentOkr(Guid okrId, UpdateDepartmentsDTO dto)
        {
            var okr = await _context.OKRs
                .Include(o => o.OkrDepartments)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            if (okr == null)
                throw new Exception("Không tìm thấy OKR");

            // Xóa các phòng ban cũ
            _context.OkrDepartments.RemoveRange(okr.OkrDepartments);

            // Thêm lại phòng ban mới
            foreach (var deptId in dto.DepartmentIds.Distinct())
            {
                _context.OkrDepartments.Add(new OkrDepartment
                {
                    OkrId = okrId,
                    DepartmentId = deptId
                });
            }

            await _context.SaveChangesAsync();
        }



        public async Task<Response> UpdateOwnerOkr(Guid okrId, UpdatePeopleDTO dto)
        {
            var okr = await _context.OKRs
                .Include(o => o.OkrUsers)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            if (okr == null)
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };

            // Thay thế set cho từng role nếu có gửi lên
            ReplaceUsersForRole(okr, OkrRoles.Owner, dto.OwnerIds);
            ReplaceUsersForRole(okr, OkrRoles.Manager, dto.ManagerIds);

            okr.LastUpdated = DateTime.UtcNow.AddHours(7);

            await _context.SaveChangesAsync();

            // (tuỳ chọn) ghi lịch sử thay đổi people
            var userIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userIdStr))
            {
                _context.okrHistories.Add(new OkrHistory
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(userIdStr),
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    OldProgress = okr.Progress,
                    NewProgress = okr.Progress,
                    OldAchieved = okr.Achieved,
                    NewAchieved = okr.Achieved,
                    UnitOfTarget = okr.UnitOfTarget,
                    OldStatus = okr.Status,
                    NewStatus = okr.Status,
                    Type = "logging",
                    Description = "Update people (owners/managers)",
                    OkrId = okr.Id
                });
                await _context.SaveChangesAsync();
            }

            return new Response { Code = ResponseCode.Success, Message = "Update people successfully!" };
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

        public async Task<Response> UpdateOkr(Guid okrId, OKRPartialUpdateDTO dto)
        {
            var okr = await _context.OKRs
                .Include(o => o.OkrUsers)
                .Include(o => o.OkrDepartments)
                .FirstOrDefaultAsync(o => o.Id == okrId);

            if (okr == null)
                return new Response { Code = ResponseCode.NotFound, Message = "Objective not found." };

            // Gán field nếu có
            if (dto.Title != null) okr.Title = dto.Title;
            if (dto.Content != null) okr.Content = dto.Content;
            if (dto.Type != null) okr.Type = dto.Type;
            if (dto.Scope != null) okr.Scope = dto.Scope;
            if (dto.TargetNumber.HasValue) okr.TargetNumber = dto.TargetNumber.Value;
            if (dto.UnitOfTarget != null) okr.UnitOfTarget = dto.UnitOfTarget;
            if (dto.Cycle != null) okr.Cycle = dto.Cycle;
            if (dto.ConfidenceLevel != null) okr.ConfidenceLevel = dto.ConfidenceLevel;
            if (dto.Result != null) okr.Result = dto.Result;
            if (dto.ParentId.HasValue) okr.ParentId = dto.ParentId;
            if (dto.DueDate.HasValue) okr.DueDate = dto.DueDate;
            if (dto.Priority != null) okr.Priority = dto.Priority;
            if (dto.Company != null) okr.Company = dto.Company;
            if (dto.Note != null) okr.Note = dto.Note;

            // RULE: Progress/Achieved chỉ cho “công việc cá nhân”
            var isPersonal = string.Equals(okr.Scope, "Cá nhân", StringComparison.OrdinalIgnoreCase)
                             || string.Equals(okr.Scope, "Personal", StringComparison.OrdinalIgnoreCase)
                             || string.Equals(okr.Type, "Cá nhân", StringComparison.OrdinalIgnoreCase)
                             || string.Equals(okr.Type, "Personal", StringComparison.OrdinalIgnoreCase);

            if (dto.Achieved.HasValue && isPersonal)
            {
                var achieved = dto.Achieved.Value;
                if (okr.TargetNumber > 0 && achieved <= okr.TargetNumber)
                {
                    okr.Achieved = achieved;
                    okr.Progress = (int)Math.Round((double)achieved / okr.TargetNumber * 100);
                    okr.Status = GetStatusBasedOnProgress(okr.Progress); // đã có sẵn helper này
                }
                else
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Invalid Achieved/TargetNumber." };
                }
            }

            // (Tuỳ bạn) Cho phép override Status thủ công?
            if (dto.Status != null)
            {
                okr.Status = dto.Status;
            }

            okr.LastUpdated = DateTime.UtcNow.AddHours(7);

            _context.OKRs.Update(okr);
            await _context.SaveChangesAsync();

            // (Khuyến nghị) Ghi lịch sử thay đổi tương tự UpdateProgressOkr
            // ... thêm OkrHistory ở đây nếu cần

            // Cập nhật progress cha nếu liên quan
            await UpdateParentOkrProgress(okr.ParentId);

            return new Response { Code = ResponseCode.Success, Message = "Update successfully!" };
        }


        

        // Map entity -> OKRDTO cho cả list & detail
        private OKRDTO MapToOkrDto(OKR o)
        {
            string Norm(string? s) => string.IsNullOrWhiteSpace(s) ? "" : s;

            var owners = o.OkrUsers.Where(ou => ou.Role == OkrRoles.Owner).ToList();
            var managers = o.OkrUsers.Where(ou => ou.Role == OkrRoles.Manager).ToList();
            var creators = o.OkrUsers.Where(ou => ou.Role == OkrRoles.Creator).ToList();

            var ownerIds = owners.Select(ou => ou.UserId).Distinct().ToList();
            var ownerNames = owners
                .Select(ou => Norm(ou.User?.UserInformation?.FullName))
                .Where(n => n.Length > 0)
                .Distinct()
                .ToList();

            var managerNames = managers
                .Select(ou => Norm(ou.User?.UserInformation?.FullName))
                .Where(n => n.Length > 0)
                .Distinct()
                .ToList();

            var creator = creators.FirstOrDefault();
            var createdById = creator?.UserId;
            var createdByName = Norm(creator?.User?.UserInformation?.FullName);

            var deptNames = o.OkrDepartments
                .Select(od => Norm(od.Department?.Name))
                .Where(n => n.Length > 0)
                .Distinct()
                .ToList();

            var managerIds = managers.Select(ou => ou.UserId).Distinct().ToList();
            var deptIds = o.OkrDepartments.Select(od => od.DepartmentId).Distinct().ToList();


            return new OKRDTO
            {
                Id = o.Id,
                Title = o.Title,
                Content = o.Content,
                Type = o.Type,
                Scope = o.Scope,

                OwnerId = ownerIds,
                OwnerNames = ownerNames,

                ManagerNames = managerNames,
                ManagerIds = managerIds,

                DepartmentName = deptNames,
                DepartmentIds = deptIds,

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
                    ? _context.OKRs.Where(x => x.Id == o.ParentId.Value).Select(x => x.Title).FirstOrDefault()
                    : null,
                ParentId = o.ParentId,

                CreatedById = createdById,
                CreatedByName = string.IsNullOrEmpty(createdByName) ? null : createdByName,
            };
        }


    }
}

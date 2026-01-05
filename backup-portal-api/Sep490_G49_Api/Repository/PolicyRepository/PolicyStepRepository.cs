// Repository/PolicyRepository/PolicyStepRepository.cs
using AutoMapper;
using BusinessObjects.DTO.Policy;
using BusinessObjects.DTO.WorkFlow;
using BusinessObjects.Files;        // IFileStorage của bạn
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;

namespace Repository.PolicyRepository
{
    public class PolicyStepRepository : IPolicyStepRepository
    {
        private readonly SEP490_G49Context _ctx;
        private readonly IMapper _mapper;
        private readonly IFileStorage _files;

        public PolicyStepRepository(SEP490_G49Context ctx, IMapper mapper, IFileStorage files)
        {
            _ctx = ctx; _mapper = mapper; _files = files;
        }

        public async Task<PaginatedList<PolicyStepRowDTO>> GetAllSteps(int pageIndex, int pageSize, string? q)
        {
           

            IQueryable<PolicyStep> qb = _ctx.PolicySteps.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.ToLower();
                qb = qb.Where(x => (x.Code ?? "").ToLower().Contains(s)
                                || (x.Title ?? "").ToLower().Contains(s));
            }

            // Include sau cũng được, EF vẫn build 1 query
            qb = qb
                .Include(x => x.PolicyStepUsers)
                    .ThenInclude(psu => psu.User)
                    .ThenInclude(u => u.UserInformation)
                .Include(x => x.PolicyStepDepartments)
                    .ThenInclude(d => d.Department);

            var total = await qb.CountAsync();
            var items = await qb.OrderBy(x => x.OrderIndex)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            var rows = new List<PolicyStepRowDTO>();
            foreach (var st in items)
            {
                var dto = new PolicyStepRowDTO
                {
                    Id = st.Id,
                    Code = st.Code,
                    Title = st.Title,
                    Content = st.Note,
                    ExecDate = st.ExecDate,
                    ReviewDate = st.ReviewDate,
                    ApproveDate = st.ApproveDate,
                    LawRef = st.LawRef,
                    LatestSubmission = await LoadLatestSubmissionAsync(st.Id),
                    ParentId = st.ParentId,
                };

                dto.Executors = st.PolicyStepUsers.Where(r => r.Role == "Executor")
                    .OrderBy(r => r.OrderIndex)
                    .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                    .ToList();
                dto.Reviewers = st.PolicyStepUsers.Where(r => r.Role == "Reviewer")
                    .OrderBy(r => r.OrderIndex)
                    .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                    .ToList();
                dto.Approvers = st.PolicyStepUsers.Where(r => r.Role == "Approver")
                    .OrderBy(r => r.OrderIndex)
                    .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                    .ToList();

                dto.ExecutorDepartments = st.PolicyStepDepartments.Where(d => d.Role == "Executor")
                    .Select(d => new PolicyStepDepartmentDTO { DepartmentId = d.DepartmentId, Name = d.Department.Name })
                    .ToList();
                dto.ReviewerDepartments = st.PolicyStepDepartments.Where(d => d.Role == "Reviewer")
                    .Select(d => new PolicyStepDepartmentDTO { DepartmentId = d.DepartmentId, Name = d.Department.Name })
                    .ToList();
                dto.ApproverDepartments = st.PolicyStepDepartments.Where(d => d.Role == "Approver")
                    .Select(d => new PolicyStepDepartmentDTO { DepartmentId = d.DepartmentId, Name = d.Department.Name })
                    .ToList();

                rows.Add(dto);
            }

            var pages = (int)Math.Ceiling(total / (double)pageSize);
            return new PaginatedList<PolicyStepRowDTO>(rows, pageIndex, pages, total);
        }

        // Đổi sang PolicyStepRowDTO cho chắc chắn có type (nếu bạn chưa tạo PolicyStepDetailDTO)
        public async Task<Response> GetStepDetail(Guid id)
        {
            IQueryable<PolicyStep> qb = _ctx.PolicySteps.AsNoTracking()
                .Where(x => x.Id == id);

            qb = qb
                .Include(x => x.PolicyStepUsers).ThenInclude(psu => psu.User).ThenInclude(u => u.UserInformation)
                .Include(x => x.Documents);

            var st = await qb.FirstOrDefaultAsync();
            if (st == null) return new Response { Code = ResponseCode.NotFound, Message = "Step not found" };

            var dto = new PolicyStepRowDTO
            {
                Id = st.Id,
                Code = st.Code,
                Title = st.Title,
                Content = st.Note,
                ExecDate = st.ExecDate,
                ReviewDate = st.ReviewDate,
                ApproveDate = st.ApproveDate,
                LawRef = st.LawRef,

            };

            dto.Executors = st.PolicyStepUsers.Where(r => r.Role == "Executor")
                .OrderBy(r => r.OrderIndex)
                .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                .ToList();

            dto.Reviewers = st.PolicyStepUsers.Where(r => r.Role == "Reviewer")
                .OrderBy(r => r.OrderIndex)
                .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                .ToList();

            dto.Approvers = st.PolicyStepUsers.Where(r => r.Role == "Approver")
                .OrderBy(r => r.OrderIndex)
                .Select(r => new PolicyUserTagDTO { UserId = r.UserId, Name = r.User.UserInformation.FullName })
                .ToList();

            dto.LatestSubmission = await LoadLatestSubmissionAsync(st.Id);

            // Nếu bạn đã tạo PolicyStepDetailDTO có property Documents, có thể map thêm phần này.
            // Còn nếu chưa có, comment lại để compile OK.
            // dto.Documents = st.Documents.OrderByDescending(d => d.UploadedAt)
            //     .Select(d => new PolicyDocumentDTO
            //     {
            //         Id = d.Id,
            //         FileName = d.FileName,
            //         FileSize = d.FileSize,
            //         UploadedAt = d.UploadedAt,
            //         Category = d.Category,
            //         DownloadUrl = _files.BuildDownloadUrl(d.StoragePath),
            //         UploadedByName = GetUserName(d.UploadedBy)
            //     }).ToList();

            return new Response { Code = ResponseCode.Success, Data = dto };
        }

        public async Task<Response> UpdateStep(Guid id, UpdatePolicyStepDTO dto)
        {
            var st = await _ctx.PolicySteps
                .Include(x => x.PolicyStepUsers)
                .Include(x => x.PolicyStepDepartments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (st == null) return new Response { Code = ResponseCode.NotFound, Message = "Step not found" };

            if (dto.ExecDate.HasValue) st.ExecDate = dto.ExecDate;
            if (dto.ReviewDate.HasValue) st.ReviewDate = dto.ReviewDate;
            if (dto.ApproveDate.HasValue) st.ApproveDate = dto.ApproveDate;
            if (dto.LawRef != null) st.LawRef = dto.LawRef;
            if (dto.Content != null) st.Note = dto.Content;

            // replace USERS theo role
            async Task ReplaceUsersAsync(string role, List<Guid>? ids)
            {
                if (ids == null) return;
                var olds = st.PolicyStepUsers.Where(u => u.Role == role).ToList();
                _ctx.PolicyStepUsers.RemoveRange(olds);
                int i = 0;
                foreach (var uid in ids.Distinct())
                {
                    _ctx.PolicyStepUsers.Add(new PolicyStepUser
                    {
                        Id = Guid.NewGuid(),
                        PolicyStepId = st.Id,
                        UserId = uid,
                        Role = role,
                        OrderIndex = i++,
                        AssignedAt = DateTime.UtcNow
                    });
                }
            }

            // replace DEPARTMENTS theo role (NEW)
            async Task ReplaceDeptsAsync(string role, List<Guid>? deptIds)
            {
                if (deptIds == null) return;
                var olds = st.PolicyStepDepartments.Where(d => d.Role == role).ToList();
                _ctx.PolicyStepDepartments.RemoveRange(olds);
                int i = 0;
                foreach (var did in deptIds.Distinct())
                {
                    _ctx.PolicyStepDepartments.Add(new PolicyStepDepartment
                    {
                        Id = Guid.NewGuid(),
                        PolicyStepId = st.Id,
                        DepartmentId = did,
                        Role = role,
                    });
                }
            }

            await ReplaceUsersAsync("Executor", dto.ExecutorIds);
            await ReplaceUsersAsync("Reviewer", dto.ReviewerIds);
            await ReplaceUsersAsync("Approver", dto.ApproverIds);

            await ReplaceDeptsAsync("Executor", dto.ExecutorDepartmentIds);
            await ReplaceDeptsAsync("Reviewer", dto.ReviewerDepartmentIds);
            await ReplaceDeptsAsync("Approver", dto.ApproverDepartmentIds);

            await _ctx.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Updated" };
        }

        private async Task<LatestSubmissionDTO?> LoadLatestSubmissionAsync(Guid stepId)
        {
            var sub = await _ctx.Submissions.AsNoTracking()
                .Where(s => s.PolicyStepId == stepId)
                .OrderByDescending(s => s.LastUpdated ?? s.CreatedAt)
                .FirstOrDefaultAsync();
            if (sub == null) return null;

            var timeline = await _ctx.SubmissionEvents.AsNoTracking()
                .Where(e => e.SubmissionId == sub.Id)
                .ToListAsync();

            DateTime? t(string a) => timeline.FirstOrDefault(x => x.Action == a)?.At;

            return new LatestSubmissionDTO
            {
                Id = sub.Id,
                Status = sub.Status == "ForApproval" ? "Resubmitted" : sub.Status,
                CreatedAt = sub.CreatedAt,
                LastUpdated = sub.LastUpdated,
                SubmittedAt = t("Submit") ?? (sub.Status != "Draft" ? sub.CreatedAt : null),
                InReviewAt = t("StartReview"),
                NeedsChangesAt = t("RequestChanges"),
                ForApprovalAt = t("Pass"),
                ApprovedAt = t("Approve"),
                RejectedAt = t("Reject"),
            };
        }
    }
}

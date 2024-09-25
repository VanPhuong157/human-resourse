using AutoMapper;
using BusinessObjects.DTO.JobPost;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccess.JobPosts
{
    public class JobPostDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobPostDAO(SEP490_G49Context context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedList<JobPostDTO>> GetJobPosts(
            int pageIndex = 1,
            int pageSize = 5,
            string? title = null,
            int? minSalary = null,
            int? maxSalary = null,
            int? minYear = null,
            int? maxYear = null,
            string? type = null,
            string? status = null,
            Guid? departmentId = null)
        {
            var query = _context.JobPosts
               .Include(x => x.Department)
                .Where(x => x.IsDelete == false).AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(j => j.Title.Contains(title));
            }

            if (minSalary.HasValue)
            {
                query = query.Where(j => j.Salary >= minSalary.Value);
            }

            if (maxSalary.HasValue)
            {
                query = query.Where(j => j.Salary <= maxSalary.Value);
            }

            if (minYear.HasValue)
            {
                query = query.Where(j => j.ExperienceYear >= minYear.Value);
            }

            if (maxYear.HasValue)
            {
                query = query.Where(j => j.ExperienceYear <= maxYear.Value);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(j => j.Type == type);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(j => j.Status == status);
            }
            if (departmentId.HasValue)
            {
                query = query.Where(j => j.DepartmentId == departmentId.Value);
            }
            var jobs = await query
                .OrderByDescending(b => b.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            var userIds = jobs.Select(job => job.CreatedBy).Distinct().ToList();
            var users = await _context.Users
        .Where(u => userIds.Contains(u.Id))
        .Select(u => new { u.Id, FullName = u.UserInformation.FullName })
        .ToDictionaryAsync(u => u.Id, u => u.FullName);
            var jobList = jobs.Select(job => new JobPostDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CreatedDate = job.CreatedDate,
                ExpiryDate = job.ExpiryDate,
                NumberOfRecruits = job.NumberOfRecruits,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExperienceYear = job.ExperienceYear,
                Type = job.Type,
                Status = job.Status,
                IsDelete = job.IsDelete,
                DepartmentId = job.DepartmentId,
                DepartmentName = job.Department?.Name,
                CreatedBy = job.CreatedBy.HasValue && users.TryGetValue(job.CreatedBy.Value, out var fullName) ? fullName : null
            }).ToList();

            return new PaginatedList<JobPostDTO>(jobList, pageIndex, totalPages, count);
        }

        public async Task<JobPostDTO> GetJobPostById(Guid id)
        {
            var job = await _context.JobPosts
                .Include(x => x.Department)
                    .ThenInclude(d => d.Users) // Bao gồm thông tin người dùng trong phòng ban
                    .ThenInclude(u => u.UserInformation) // Bao gồm thông tin người dùng
                .FirstOrDefaultAsync(x => x.Id == id);

            if (job == null) throw new Exception("Job not found");

            // Tạo danh sách các userId từ job
            var userIds = new List<Guid?> { job.CreatedBy }.Where(id => id.HasValue).Select(id => id.Value).Distinct().ToList();
            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new { u.Id, FullName = u.UserInformation.FullName })
                .ToDictionaryAsync(u => u.Id, u => u.FullName);

            // Ánh xạ dữ liệu vào DTO
            var jobDetails = new JobPostDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CreatedDate = job.CreatedDate,
                ExpiryDate = job.ExpiryDate,
                NumberOfRecruits = job.NumberOfRecruits,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExperienceYear = job.ExperienceYear,
                Type = job.Type,
                Status = job.Status,
                IsDelete = job.IsDelete,
                DepartmentId = job.DepartmentId,
                DepartmentName = job.Department?.Name,
                CreatedBy = job.CreatedBy.HasValue && users.TryGetValue(job.CreatedBy.Value, out var fullName) ? fullName : null
            };

            return jobDetails;
        }


        public async Task<Response> EditJobPost(Guid id, JobPostRequestDTO jobDetails)
        {
            try
            {
                var job = await _context.JobPosts
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
                var department = await _context.Departments.FindAsync(jobDetails.Department);
                if (job == null) return new Response { Code = ResponseCode.NotFound, Message = "Not found job" };
                job.Title = jobDetails.Title;
                job.NumberOfRecruits = jobDetails.NumberOfRecruits;
                job.Benefits = jobDetails.Benefits;
                job.Requirements = jobDetails.Requirements;
                job.ExperienceYear = jobDetails.ExperienceYear;
                job.DepartmentId = jobDetails.Department;
                job.Description = jobDetails.Description;
                job.Type = jobDetails.Type;
                job.Status = jobDetails.Status;
                job.ExpiryDate = jobDetails.ExpiryDate;
                job.Salary = jobDetails.Salary;
                job.Department = department;
                _context.JobPosts.Update(job);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Edit failure!" };
            }
            
        }

        public async Task<Response> CreatePost(JobPostRequestDTO jobDetails)
        {
            try
            {
                if (jobDetails == null)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Request is null." };
                }

                var department = await _context.Departments.FindAsync(jobDetails.Department);
                if (department == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Department not found." };
                }
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "User ID claim is missing." };
                }
                var newPost = new JobPost
                {
                    Id = Guid.NewGuid(),
                    Title = jobDetails.Title,
                    Description = jobDetails.Description,
                    Benefits = jobDetails.Benefits,
                    CreatedDate = DateTime.UtcNow.AddHours(7),
                    ExperienceYear = jobDetails.ExperienceYear,
                    ExpiryDate = jobDetails.ExpiryDate,
                    NumberOfRecruits = jobDetails.NumberOfRecruits,
                    Requirements = jobDetails.Requirements,
                    Type = jobDetails.Type,
                    Status = jobDetails.Status,
                    Salary = jobDetails.Salary,
                    IsDelete = false,
                    DepartmentId = jobDetails.Department,
                    Department = department,
                    CreatedBy = Guid.Parse(userId)
                };

                _context.JobPosts.Add(newPost);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Create failure!" };
            }
        }
        public int GetTotalRecruitingJobPostCount(Guid? departmentId)
        {
            return _context.JobPosts
                .Where(j => !j.IsDelete &&
                            j.Status == "Recruiting" &&
                            (!departmentId.HasValue || j.DepartmentId == departmentId.Value))
                .Count();
        }

        public double CalculateJobPostGrowthPercentage(Guid? departmentId)
        {
            var currentDate = DateTime.UtcNow.AddHours(7);
            var currentMonthCount = _context.JobPosts
                .Where(j => !j.IsDelete &&
                            j.Status == "Recruiting" &&
                            (!departmentId.HasValue || j.DepartmentId == departmentId.Value) &&
                            j.CreatedDate.Month == currentDate.Month &&
                            j.CreatedDate.Year == currentDate.Year)
                .Count();

            var previousMonthDate = currentDate.AddMonths(-1);
            var previousMonthCount = _context.JobPosts
                .Where(j => !j.IsDelete &&
                            j.Status == "Recruiting" &&
                            (!departmentId.HasValue || j.DepartmentId == departmentId.Value) &&
                            j.CreatedDate.Month == previousMonthDate.Month &&
                            j.CreatedDate.Year == previousMonthDate.Year)
                .Count();

            if (previousMonthCount == 0)
            {
                return currentMonthCount == 0 ? 0 : 100;
            }

            return ((double)(currentMonthCount - previousMonthCount) / previousMonthCount) * 100;
        }

    }
}

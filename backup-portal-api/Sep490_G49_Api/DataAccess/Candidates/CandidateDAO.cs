using AutoMapper;
using BusinessObjects.DTO.Candidate;
using BusinessObjects.DTO.Statistic;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Emails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Candidates
{
    public class CandidateDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly EmailDAO _emailDAO;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public CandidateDAO(SEP490_G49Context context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> notificationHubContext, EmailDAO emailDAO)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _notificationHubContext = notificationHubContext;
            _emailDAO = emailDAO;
        }
        public async Task<Candidate?> GetCandidateByIdAsync(Guid id)
        {
            return await _context.Candidates
                .Include(c => c.JobPost)  // Nạp thêm thông tin của JobPost nếu cần
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<PaginatedList<CandidateResponseDTO>> GetCandidates(
            int pageIndex = 1,
    int pageSize = 10,
    string? name = null,
    string? email = null,
    string? phoneNumber = null,
    string? status = null,
    string? startDateApply = null, 
    string? endDateApply = null,  
    Guid? jobPostId = null)
        {
            var query = _context.Candidates
                .Where(x => x.IsDeleted == false)
                .Include(c => c.JobPost)
                .AsQueryable();

            if (jobPostId.HasValue)
            {
                query = query.Where(c => c.JobPostId == jobPostId.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FullName.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(c => c.PhoneNumber.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(c => c.Status == status);
            }

            DateTime? startDateApplyDT = null;
            DateTime? endDateApplyDT = null;

            // Chuyển đổi chuỗi thành DateTime
            if (!string.IsNullOrEmpty(startDateApply))
            {
                startDateApplyDT = DateTime.ParseExact(startDateApply, "dd/MM/yyyy", null);
            }

            if (!string.IsNullOrEmpty(endDateApply))
            {
                endDateApplyDT = DateTime.ParseExact(endDateApply, "dd/MM/yyyy", null);
            }

            if (startDateApplyDT.HasValue)
            {
                query = query.Where(c => c.DateApply >= startDateApplyDT.Value);
            }

            if (endDateApplyDT.HasValue)
            {
                query = query.Where(c => c.DateApply <= endDateApplyDT.Value);
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var candidates = await query
                .OrderByDescending(c => c.DateApply)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var candidateList = _mapper.Map<List<CandidateResponseDTO>>(candidates);

            return new PaginatedList<CandidateResponseDTO>(candidateList, pageIndex, totalPages, count);
        }

        public async Task<Response> CreateCandidate(CandidateDTO candidateDTO)
        {
            string? uniqueFileName = null;
            try
            {
                if (candidateDTO.CvFile != null)
                {
                    var allowedExtensions = new[] { ".pdf" };
                    var fileExtension = Path.GetExtension(candidateDTO.CvFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                    }

                    var allowedMimeTypes = new[] { "application/pdf" };
                    if (!allowedMimeTypes.Contains(candidateDTO.CvFile.ContentType))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only PDF files are allowed." };
                    }

                    var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Candidates", candidateDTO.FullName);
                    Directory.CreateDirectory(uploadsFolder);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + candidateDTO.CvFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await candidateDTO.CvFile.CopyToAsync(fileStream);
                    }
                }
                var jobPost = await _context.JobPosts.FirstOrDefaultAsync(x => x.Id == candidateDTO.JobPostId);
                var candidate = _mapper.Map<Candidate>(candidateDTO);
                candidate.Id = Guid.NewGuid();
                candidate.DateApply = DateTime.UtcNow.AddHours(7);
                candidate.IsDeleted = false;
                candidate.Status = "Pending";
                candidate.CvDetail = uniqueFileName != null ? $"/Uploads/Candidates/{candidateDTO.FullName}/{uniqueFileName}" : null;
                candidate.Cv = candidateDTO.CvFile?.FileName;
                candidate.JobPostId = candidateDTO.JobPostId;
                candidate.JobPost = jobPost;
                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                var message = $"New Candidate applies for Job '{jobPost.Title}'";
                var redirectUrl = "";
                var user = _context.Users.FirstOrDefault(x => x.Id == jobPost.CreatedBy);
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsRead = false,
                    UserId = user.Id,
                    RedirectUrl = redirectUrl,
                    User = user
                };

                _context.Notifications.Add(notification);
                _context.SaveChanges();

                await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification", jobPost.CreatedBy, new Notification
                {
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    Message = message,
                    RedirectUrl = redirectUrl
                });

                var homepage = await _context.HomePages.FirstOrDefaultAsync(h => h.Status == "Active");
                string subject = "SHRS Company - APPLICATION SUCCESSFUL";
                string body = $@"
            Dear {candidate.FullName},

            The Recruitment Department of <strong>SHRS Company</strong> sincerely thanks you for your interest and participation in applying for the {jobPost.Title} position.

            <strong>SHRS Company</strong> will review your application and will contact you with further training plans as soon as possible.

            Please keep an eye on your phone/email for notifications from <strong>SHRS Company</strong>.

            Wishing you health and success!

            Regards,

            If you have any questions, please contact us at:
            <p>
        <strong>Mobile:</strong> {homepage.PhoneNumber}<br>
        <strong>Email:</strong> {homepage.Email}<br>
    </p>
            ";

                await _emailDAO.SendEmail(candidate.Email, subject, body);

                return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Create failure!" };
            }
        }
        private async Task NotifyDepartmentMembers(Guid departmentId, string message, string redirectUrl = null)
        {
            try
            {
                var department = await _context.Departments
                    .Include(d => d.Users)
                    .FirstOrDefaultAsync(d => d.Id == departmentId);

                if (department == null)
                {
                    throw new Exception("Department not found.");
                }

                foreach (var user in department.Users)
                {
                    //await _notificationHubContext.Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", new Notification
                    //{
                    //    Message = message,
                    //    RedirectUrl = redirectUrl
                    //});


                    // Add notification to database
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        Message = message,
                        CreatedAt = DateTime.UtcNow.AddHours(7),
                        IsRead = false,
                        UserId = user.Id,
                        RedirectUrl = redirectUrl,
                        User = user
                    };

                    _context.Notifications.Add(notification);
                    await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification", user.Id, new Notification
                    {
                        CreatedAt = notification.CreatedAt,
                        Message = message,
                        RedirectUrl = redirectUrl
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Failed to notify department members.", ex);
            }
        }
        public async Task<Response> EditStatus(Guid id, CandidateUpdateStatusDTO candidateUpdateStatusDTO)
        {
            try
            {
                var candidate = await _context.Candidates.Include(c=>c.JobPost).FirstOrDefaultAsync(c => c.Id == id);
                var homepage = await _context.HomePages.FirstOrDefaultAsync(h => h.Status == "Active");
                if (candidate == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found candidate." };
                }
                candidate.Status = candidateUpdateStatusDTO.NewStatus;

                if (candidateUpdateStatusDTO.NewStatus == "Pass")
                {
                    var subjectPass = "APPLICATION RESULTS";
                    var bodyPass = $@"
       <div style=""font-family: Arial, sans-serif; color: #333;"">
    <p>Dear <strong>{candidate.FullName}</strong>,</p>
    <p>
        First of all, we would like to thank you for taking the time to apply for the position of 
        <strong>{candidate.JobPost.Title}</strong> at our company.
    </p>
    <p>
        Throughout our meetings and discussions, we were very impressed with your experience and what you have demonstrated. 
        Therefore, we are honored to invite you to join our company as <strong>{candidate.JobPost.Title}</strong>.
    </p>
    <p>Please find the details below:</p>
    <table style=""border-collapse: collapse; width: 100%; margin: 20px 0;"">
        <tr>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;"">Position:</td>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd;"">{candidate.JobPost.Title}</td>
        </tr>
        <tr>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;"">Address:</td>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd;"">{homepage.Address}</td>
        </tr>
        <tr>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;"">Phone Number:</td>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd;"">{homepage.PhoneNumber}</td>
        </tr> 
        <tr>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;"">Email:</td>
            <td style=""padding: 8px; border-bottom: 1px solid #ddd;"">{homepage.Email}</td>
        </tr>
    </table>
    <p style=""margin-top: 20px;"">We look forward to your positive response.</p>
    <p>Best regards,<br><strong>SHRS Company</strong></p>
</div>

";
                    var sendEmail = _emailDAO.SendEmail(candidate.Email, subjectPass, bodyPass);
                }

                if (candidateUpdateStatusDTO.NewStatus == "NotPass")
                {
                    var subjectNotPass = "APPLICATION RESULTS";
                    var bodyNotPass = $@"
      <div style=""font-family: Arial, sans-serif; color: #333;"">
    <p>Dear <strong>{candidate.FullName}</strong>,</p>
    <p>
        <strong>SHRS Company</strong> would like to sincerely thank you for participating in the recruitment process for the 
        <strong>{candidate.JobPost.Title}</strong> position. Throughout the selection process, we greatly appreciated your enthusiasm 
        for <strong>SHRS Company</strong>. However, at this stage, the position <strong>SHRS Company</strong> is hiring for is not quite a match for you. 
        We will keep your information on file and would be very pleased to welcome you in future recruitment drives.
    </p>
    <p>We wish you good health and success!</p>
    <p>Sincerely,</p>
    <p><strong>SHRS Corporation</strong></p>
    <p>
        <strong>Address:</strong> {homepage.Address}<br>
        <strong>Mobile:</strong> {homepage.PhoneNumber}<br>
        <strong>Email:</strong> {homepage.Email}<br>
    </p>
</div>


";
                    var sendEmail = _emailDAO.SendEmail(candidate.Email, subjectNotPass, bodyNotPass);
                }

                if (candidateUpdateStatusDTO.NewStatus == "Interview")
                {
                    // New email content for Interview status
                    var subjectInterview = "INVITATION TO INTERVIEW";
                    var bodyInterview = $@"
                <div style=""font-family: Arial, sans-serif; color: #333;"">
                    <p>Dear <strong>{candidate.FullName}</strong>,</p>
                    <p>
                        Congratulations on passing the preliminary round for the position of 
                        <strong>{candidate.JobPost.Title}</strong> at <strong>SHRS Company</strong>. This achievement shows that you have the skills 
                        and experience that match our requirements.
                    </p>
                    <p>We are pleased to see your interest in <strong>SHRS Company</strong> and look forward to working with you in the next rounds of recruitment.</p>
                    <p>
                        We will contact you shortly to schedule the next round of interviews. If you have any questions, please contact us at 
                        <strong>{homepage.Email}</strong> or <strong>{homepage.PhoneNumber}</strong>.
                    </p>
                    <p>Once again, congratulations on this achievement. We look forward to meeting you in the upcoming interview round.</p>
                    <p>Best regards,</p>
                    <p><strong>{homepage.Email}</strong><br><strong>{homepage.PhoneNumber}</strong><br><strong>{homepage.Address}</strong></p>
                </div>";

                    await _emailDAO.SendEmail(candidate.Email, subjectInterview, bodyInterview);
                }

                await _context.SaveChangesAsync();

              
                return new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Edit failure!" };
            }
        }

        public async Task<string?> GetCvDetailById(Guid id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null || candidate.IsDeleted)
            {
                throw new Exception("Candidate not found or has been deleted");
            }
            return candidate.CvDetail;
        }
        public async Task<Response> DeleteCandidate(Guid id)
        {
            try
            {
                var candidate = await _context.Candidates.FindAsync(id);
                if (candidate == null || candidate.IsDeleted)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Candidate not found or already deleted." };
                }
                candidate.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response { Code = ResponseCode.Success, Message = "Delete successfully!" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Delete failure!" };
            }
        }
        public int CountCandidatesNotPassedByDepartment(Guid? departmentId)
        {
            using (var context = new SEP490_G49Context())
            {
                var query = context.Candidates.Include(x => x.JobPost).ThenInclude(x => x.Department)
                    .Where(c => c.Status != "Pass" && c.Status != "Onboarding" && !c.IsDeleted);

                if (departmentId.HasValue)
                {
                    query = query.Where(c => c.JobPost.DepartmentId == departmentId);
                }

                return query.Count();
            }
        }

        //% tăng trưởng=(năm cần tính – năm trước)/năm trước*100
        public double CalculatePercentageDifferenceApplyDate(Guid? departmentId)
        {
            using (var context = new SEP490_G49Context())
            {
                var currentDate = DateTime.UtcNow.AddHours(7);

                var currentMonthQuery = context.Candidates.Include(x => x.JobPost).ThenInclude(x => x.Department)
                    .Where(c => !c.IsDeleted && c.DateApply.Month == currentDate.Month && c.DateApply.Year == currentDate.Year);

                var previousMonthDate = currentDate.AddMonths(-1);
                var previousMonthQuery = context.Candidates.Include(x => x.JobPost).ThenInclude(x => x.Department)
                    .Where(c => !c.IsDeleted && c.DateApply.Month == previousMonthDate.Month && c.DateApply.Year == previousMonthDate.Year);

                if (departmentId.HasValue)
                {
                    currentMonthQuery = currentMonthQuery.Where(c => c.JobPost.DepartmentId == departmentId);
                    previousMonthQuery = previousMonthQuery.Where(c => c.JobPost.DepartmentId == departmentId);
                }

                var currentMonthCount = currentMonthQuery.Count();
                var previousMonthCount = previousMonthQuery.Count();

                if (previousMonthCount == 0)
                {
                    return currentMonthCount == 0 ? 0 : 100;
                }

                return ((double)(currentMonthCount - previousMonthCount) / previousMonthCount) * 100;
            }
        }
        public List<JobPostCandidateCountDTO> GetCandidateCountByJobPost(Guid? departmentId)
        {
            var query = _context.JobPosts.AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(j => j.DepartmentId == departmentId);
            }

            var result = query
                .Select(j => new JobPostCandidateCountDTO
                {
                    Title = j.Title,
                    CV = j.Candidates.Count(c => !c.IsDeleted && c.Status != "Pass" && c.Status != "Onboarding")
                })
                .ToList();

            return result;
        }
    }
}

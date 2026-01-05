// Tạo file: ScheduleRepository.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DTO.Schedule;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Schedules
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ScheduleRepository(
            SEP490_G49Context context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PaginatedList<ScheduleDTO>> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User ID not found"));
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            var isLeader = user.Role.Name == "Manager" || user.Role.Name == "Admin"; // Giả định role lãnh đạo

            var query = _context.Schedules
                .Include(s => s.Creator).ThenInclude(c => c.UserInformation)
                .Include(s => s.ApprovedBy)
                .Include(s => s.Attachments)
                .Include(s => s.Participants).ThenInclude(p => p.User).ThenInclude(u => u.UserInformation)
                .Where(s => !s.IsDeleted);

            if (!isLeader)
            {
                query = query.Where(s => s.CreatorId == userId || s.Participants.Any(p => p.UserId == userId));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var schedules = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var scheduleDTOs = _mapper.Map<List<ScheduleDTO>>(schedules);

            return new PaginatedList<ScheduleDTO>(scheduleDTOs, pageIndex, totalPages, count);
        }

        public async Task<Response> Create(CreateScheduleDTO createSchedule)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User ID not found"));

            var schedule = _mapper.Map<Schedule>(createSchedule);
            schedule.Id = Guid.NewGuid();
            schedule.CreatorId = userId;
            schedule.CreatedBy = userId;
            schedule.UpdatedBy = userId;

            // Xử lý participants
            schedule.Participants = createSchedule.ParticipantIds.Select(id => new ScheduleParticipant { UserId = id }).ToList();

            // Xử lý attachments
            if (createSchedule.Files != null && createSchedule.Files.Any())
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                foreach (var file in createSchedule.Files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadsPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        schedule.Attachments.Add(new ScheduleAttachment
                        {
                            Id = Guid.NewGuid(),
                            FileName = file.FileName,
                            StoredPath = fileName,
                            ContentType = file.ContentType
                        });
                    }
                }
            }

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Schedule created successfully" };
        }

        public async Task<Response> Approve(Guid id, ApproveScheduleDTO approve)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User ID not found"));
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || (user.Role.Name != "Manager" && user.Role.Name != "Admin"))
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Only leaders can approve schedules" };
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Schedule not found" };
            }

            schedule.Status = approve.Status;
            schedule.ApprovedById = userId;
            schedule.UpdatedAt = DateTime.UtcNow.AddHours(7);
            schedule.UpdatedBy = userId;

            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Schedule updated successfully" };
        }

        public async Task<Response> DownloadFile(string fileName)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadsPath, fileName);
            if (!File.Exists(filePath))
            {
                return new Response { Code = ResponseCode.NotFound, Message = "File not found" };
            }

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var contentType = "application/octet-stream"; // Hoặc lấy từ db nếu cần

            _httpContextAccessor.HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
            _httpContextAccessor.HttpContext.Response.ContentType = contentType;

            return new Response { Code = ResponseCode.Success, Data = fileBytes }; // Trả về byte[] trong Data, nhưng thực tế controller sẽ handle stream
        }
    }
}
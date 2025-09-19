using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Repository.OkrHistories
{
    public class OkrHistoryRepository : IOkrHistoryRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OkrHistoryRepository(SEP490_G49Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<OkrHistoryDTO>> GetOkrHistories(Guid okrId)
        {
            var orkH = await _context.okrHistories
                .Where(x => x.OkrId == okrId)
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
                    OkrId = x.OkrId
                })
                .ToListAsync();

            return orkH;
        }

        public async Task<IEnumerable<OKRHistoryCommentDTO>> GetComments(Guid okrId)
        {
            var orkH = await _context.okrHistories
                .Where(x => x.OkrId == okrId)
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new OKRHistoryCommentDTO
                {
                    UserName = _context.Users.FirstOrDefault(u => u.Id == x.UserId).UserInformation.FullName,
                    CreatedAt = x.DateCreated,
                    Description = x.Description,
                    OkrId = x.OkrId
                })
                .ToListAsync();

            return orkH;
        }

        public async Task<Response> AddComment(Guid okrId, string comment)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var okr = _context.OKRs.FirstOrDefault(o => o.Id == okrId);
            var user = await _context.Users
                .Include(u => u.UserInformation)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            var okrHistory = new OkrHistory
            {
                Id = new Guid(),
                OkrId = okrId,
                Description = comment,
                UserId = Guid.Parse(userId),
                DateCreated = DateTime.UtcNow.AddHours(7),
                OldAchieved = 0,
                NewAchieved = 0,
                UnitOfTarget = "",
                OldProgress = 0,
                NewProgress = 0,
                OldStatus = "",
                NewStatus = "",
                Type = "comment",
                OKR = okr
            };

            _context.okrHistories.Add(okrHistory);
            await _context.SaveChangesAsync();

            var okrHistoryCommentDTO = new OKRHistoryCommentDTO
            {
                UserName = user.UserInformation.FullName,
                CreatedAt = DateTime.UtcNow.AddHours(7),
                Description = okrHistory.Description,
                OkrId = okrHistory.OkrId
            };

            return new Response { Code = ResponseCode.Success, Message = "Add Comment Successfully!" };
        }

        public async Task<Response> DeleteComment(Guid okrHistoryId)
        {
            var okrHistory = await _context.okrHistories.FindAsync(okrHistoryId);
            if (okrHistory == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Not Found!" };
            }

            _context.okrHistories.Remove(okrHistory);
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Delete Comment Successfully!" }; ;
        }
    }
}

using AutoMapper;
using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UserHistories
{
    public class UserHistoryDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        public UserHistoryDAO(SEP490_G49Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<UserHistoryResponseDTO>> GetUserHistories(Guid userId)
        {
            var userHistories = await _context.UserHistories
                .Where(uh => uh.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserHistoryResponseDTO>>(userHistories);
        }

        
    }
}

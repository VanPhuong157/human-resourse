using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Response;
using DataAccess.UserHistories;

namespace Repository.UserHistories
{
    public class UserHistoryRepository : IUserHistoryRepository
    {
        private readonly UserHistoryDAO _dao;
        public UserHistoryRepository(UserHistoryDAO dao)
        {
            _dao = dao;
        }

        public async Task<IEnumerable<UserHistoryResponseDTO>> GetUserHistories(Guid userId)
        {
            return await _dao.GetUserHistories(userId);
        }
    }
}

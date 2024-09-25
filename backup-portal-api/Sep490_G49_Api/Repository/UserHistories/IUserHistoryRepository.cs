using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Response;

namespace Repository.UserHistories
{
    public interface IUserHistoryRepository
    {
        Task<IEnumerable<UserHistoryResponseDTO>> GetUserHistories(Guid userId);
    }
}

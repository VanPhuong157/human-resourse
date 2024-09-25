using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Response;
using DataAccess.OkrHistories;

namespace Repository.OkrHistories
{
    public class OkrHistoryRepository : IOkrHistoryRepository
    {
        private readonly OkrHistoryDAO okrHistoryDAO;
        public OkrHistoryRepository(OkrHistoryDAO okrHistoryDAO)
        {
            this.okrHistoryDAO = okrHistoryDAO;
        }
        public async Task<IEnumerable<OkrHistoryDTO>> GetOkrHistories(Guid okrId)
        {
            return await okrHistoryDAO.GetOkrHistories(okrId);
        }
        public async Task<Response> AddComment(Guid okrId, string comment)
        {
            return await okrHistoryDAO.AddComment(okrId, comment);
        }
        public async Task<IEnumerable<OKRHistoryCommentDTO>> GetComments(Guid okrId)
        {
            return await okrHistoryDAO.GetComment(okrId);
        }
        public async Task<Response> DeleteComment(Guid okrHistoryId)
        {
            return await okrHistoryDAO.DeleteComment(okrHistoryId);
        }
    }
}

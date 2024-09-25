using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.OkrHistories
{
    public interface IOkrHistoryRepository
    {
        Task<IEnumerable<OkrHistoryDTO>> GetOkrHistories(Guid okrId);
        Task<IEnumerable<OKRHistoryCommentDTO>> GetComments(Guid okrId);
        Task<Response> AddComment(Guid okrId, string comment);
        Task<Response> DeleteComment(Guid okrHistoryId);
    }
}

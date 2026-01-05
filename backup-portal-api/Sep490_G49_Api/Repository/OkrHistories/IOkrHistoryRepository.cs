using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
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
        Task<Response> AddComment(Guid okrId, string text, IFormFileCollection files);
        Task<Response> DeleteComment(Guid okrHistoryId);
        Task<(string PhysicalPath, string ContentType, string FileName)?> GetCommentFilePathAsync(Guid id);
    }
}

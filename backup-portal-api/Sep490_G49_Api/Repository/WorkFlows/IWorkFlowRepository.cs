using BusinessObjects.DTO.WorkFlow;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WorkFlows
{
    public interface IWorkFlowRepository
    {
        Task<Response> GetSubmission(Guid submissionId);

        Task<Response> Submit(Guid submissionId, string? note, List<IFormFile>? files);
        Task<Response> RequestChanges(Guid submissionId, string? note, Guid? toUserId); // InReview -> NeedsChanges
        Task<Response> Resubmit(Guid submissionId, string? note);       // NeedsChanges -> Submitted
        Task<Response> Pass(Guid submissionId, string? note, Guid? toUserId);          // InReview -> ForApproval
        Task<Response> Approve(Guid submissionId, string? note);        // ForApproval -> Approved (publish)
        Task<Response> Reject(Guid submissionId, string? note);         // ForApproval -> NeedsChanges/Rejected

        // Files
        Task<Response> ListFiles(Guid submissionId);
        Task<Response> UploadFile(Guid submissionId, IFormFile file, Guid uploadedBy);
        Task<Response> UpdateFile(Guid submissionId, Guid fileId, UpdateSubmissionFileDTO dto);
        Task<Response> DeleteFile(Guid submissionId, Guid fileId);

        // Comments
        Task<Response> ListComments(Guid submissionId);
        Task<Response> AddComment(Guid submissionId, string content, string byRole);
    }
}

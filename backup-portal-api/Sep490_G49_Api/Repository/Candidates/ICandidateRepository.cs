using BusinessObjects.DTO.Candidate;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.Candidates
{
    public interface ICandidateRepository
    {
        Task<PaginatedList<CandidateResponseDTO>> GetCandidates(
            int pageIndex = 1,
            int pageSize = 10,
            string? name = null,
            string? email = null,
            string? phoneNumber = null,
            string? status = null,
            string? startDateApplyStr = null,
            string? endDateApplyStr = null,
            Guid? jobPostId = null);
        Task<Response> CreateCandidate(CandidateDTO candidateDTO);
        Task<Response> EditStatus(Guid id, CandidateUpdateStatusDTO candidateUpdateStatusDTO);
        Task<string?> GetCvDetailById(Guid id);
        Task<Response> DeleteCandidate(Guid id);
        Task<Candidate?> GetCandidateByIdAsync(Guid id);
        Task<byte[]> GetCvFileAsync(string cvDetail);

    }
}


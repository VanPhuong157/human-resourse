using BusinessObjects.DTO.Candidate;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Candidates;
using Microsoft.AspNetCore.Hosting;

namespace Repository.Candidates
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDAO _candidateDAO;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CandidateRepository(CandidateDAO candidateDAO, IWebHostEnvironment webHostEnvironment)
        {
            _candidateDAO = candidateDAO;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Response> CreateCandidate(CandidateDTO candidateDTO)
        {
            return await _candidateDAO.CreateCandidate(candidateDTO);
        }
        public async Task<Candidate?> GetCandidateByIdAsync(Guid id)
        {
            return await _candidateDAO.GetCandidateByIdAsync(id);
        }

        // Lấy tệp CV từ đường dẫn
        public async Task<byte[]> GetCvFileAsync(string cvDetail)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, cvDetail.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                return await System.IO.File.ReadAllBytesAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException("CV Không có trên hệ thống.");
            }
        }

        public async Task<PaginatedList<CandidateResponseDTO>> GetCandidates(
            int pageIndex = 1,
            int pageSize = 10,
            string? name = null,
            string? email = null,
            string? phoneNumber = null,
            string? status = null,
            string? startDateApply = null,
            string? endDateApply = null,
            Guid? jobPostId = null)
        {
            return await _candidateDAO.GetCandidates(pageIndex, pageSize, name, email, phoneNumber, status, startDateApply, endDateApply, jobPostId);
        }

        public async Task<string?> GetCvDetailById(Guid id)
        {
            return await _candidateDAO.GetCvDetailById(id);
        }

        public async Task<Response> EditStatus(Guid id, CandidateUpdateStatusDTO candidateUpdateStatusDTO)
        {
            return await _candidateDAO.EditStatus(id, candidateUpdateStatusDTO);
        }

        public async Task<Response> DeleteCandidate(Guid id)
        {
            return await _candidateDAO.DeleteCandidate(id);
        }
    }
}


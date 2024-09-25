using BusinessObjects.DTO.Candidate;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Candidates;
using Microsoft.AspNetCore.Mvc;
using Repository.Candidates;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private ICandidateRepository candidateRepository;
        public CandidatesController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        [HttpGet("download-cv/{id}")]
        public async Task<IActionResult> GetCandidateCv(Guid id)
        {
            // Lấy thông tin ứng viên từ repository
            var candidate = await candidateRepository.GetCandidateByIdAsync(id);

            if (candidate == null || string.IsNullOrEmpty(candidate.CvDetail))
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "CV not found." });
            }

            try
            {
                // Lấy file CV từ repository
                var fileBytes = await candidateRepository.GetCvFileAsync(candidate.CvDetail);
                return File(fileBytes, "application/pdf", $"{candidate.FullName}_CV.pdf");
            }
            catch (FileNotFoundException)
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "CV file not found on server." });
            }
        }

        [HttpGet]
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
            return await candidateRepository.GetCandidates(
                pageIndex, pageSize, name, email, phoneNumber, status, startDateApply, endDateApply, jobPostId);
        }
        [HttpPost]
        public async Task<Response> CreateCandidate([FromForm]CandidateDTO candidateDTO)
        {
            if (candidateDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }

            try
            {
                return await candidateRepository.CreateCandidate(candidateDTO);
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = ex.Message };
            }
        }

        [HttpPut("{id}")]
        public async Task<Response> EditStatus(Guid id, CandidateUpdateStatusDTO candidateUpdateStatusDTO)
        {
            if (candidateUpdateStatusDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }

            try
            {
                return await candidateRepository.EditStatus(id, candidateUpdateStatusDTO);
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = ex.Message };
            }
        }

        [HttpGet("{candidateId}/cv-detail")]
        public async Task<string?> GetCvDetail(Guid candidateId)
        {
            return await candidateRepository.GetCvDetailById(candidateId);
        }
        [HttpDelete("{candidateId}")]
        public async Task<Response> DeleteCandidate(Guid candidateId)
        {
            return await candidateRepository.DeleteCandidate(candidateId);
        }
    }
}


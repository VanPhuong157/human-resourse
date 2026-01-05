using BusinessObjects.DTO.Policy;
using BusinessObjects.DTO.WorkFlow;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.PolicyRepository;
using Repository.WorkFlows;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/workflow")]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkFlowRepository _repo;
        private readonly IPolicyStepRepository _stepRepository;

        public WorkflowController(IWorkFlowRepository repo, IPolicyStepRepository policyStepRepository)
        {
            _repo = repo; _stepRepository = policyStepRepository;
        }

        /* ============== Steps (list/detail/update) ============== */
        [HttpGet("steps")]
        public Task<PaginatedList<PolicyStepRowDTO>> Steps(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 200,
            [FromQuery] string? q = null) => _stepRepository.GetAllSteps(pageIndex, pageSize, q);

        [HttpGet("steps/{stepId:guid}")]
        public Task<Response> StepRow(Guid stepId) => _stepRepository.GetStepDetail(stepId);

        [HttpPut("steps/{stepId:guid}")]
        public async Task<IActionResult> UpdateStep(Guid stepId, [FromBody] UpdatePolicyStepDTO dto)
        {
            var r = await _stepRepository.UpdateStep(stepId, dto);
            if (r.Code != ResponseCode.Success) return BadRequest(r);
            return Ok(r);
        }

        /* ============== Submission detail ============== */
        [HttpGet("submissions/{id:guid}")]
        public Task<Response> GetSubmission(Guid id) => _repo.GetSubmission(id);

        /* ============== Actions ============== */
        [HttpPost("submissions/{submissionId:guid}/submit")]
        [RequestSizeLimit(50 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 50 * 1024 * 1024)]
        public async Task<IActionResult> Submit(
            Guid submissionId,
            [FromForm(Name = "note")] string? note,
            [FromForm(Name = "files")] List<IFormFile>? files)
        {
            var res = await _repo.Submit(submissionId, note, files ?? new List<IFormFile>());
            return StatusCode((int)res.Code, res);
        }

        // KHÔNG cần StartReview endpoint nữa (tự động trong Pass/Back)

        public class PassOrBackRequest
        {
            public string? Note { get; set; }
            public Guid? ToUserId { get; set; }
        }

        [HttpPost("submissions/{id:guid}/pass")]
        public Task<Response> Pass(Guid id, [FromBody] PassOrBackRequest req)
            => _repo.Pass(id, req?.Note, req?.ToUserId);

        [HttpPost("submissions/{id:guid}/request-changes")]
        public Task<Response> RequestChanges(Guid id, [FromBody] PassOrBackRequest req)
            => _repo.RequestChanges(id, req?.Note, req?.ToUserId);

        [HttpPost("submissions/{id:guid}/resubmit")]
        public Task<Response> Resubmit(Guid id, [FromBody] NoteRequest req)
            => _repo.Resubmit(id, req?.Content);

        [HttpPost("submissions/{id:guid}/approve")]
        public Task<Response> Approve(Guid id, [FromBody] NoteRequest req)
            => _repo.Approve(id, req?.Content);

        [HttpPost("submissions/{id:guid}/reject")]
        public Task<Response> Reject(Guid id, [FromBody] NoteRequest req)
            => _repo.Reject(id, req?.Content);

        /* ============== Files ============== */
        [HttpGet("submissions/{id:guid}/files")]
        public Task<Response> ListFiles(Guid id) => _repo.ListFiles(id);

        [HttpPost("submissions/{id:guid}/files")]
        public Task<Response> UploadFile(Guid id, IFormFile file)
        {
            // nếu đã có auth thì đổi Guid.Empty -> userId thật
            return _repo.UploadFile(id, file, Guid.Empty);
        }

        [HttpPatch("submissions/{id:guid}/files/{fileId:guid}")]
        public Task<Response> UpdateFile(Guid id, Guid fileId, [FromBody] UpdateSubmissionFileDTO dto)
            => _repo.UpdateFile(id, fileId, dto);

        [HttpDelete("submissions/{id:guid}/files/{fileId:guid}")]
        public Task<Response> DeleteFile(Guid id, Guid fileId)
            => _repo.DeleteFile(id, fileId);

        /* ============== Comments ============== */
        [HttpGet("submissions/{id:guid}/comments")]
        public Task<Response> ListComments(Guid id) => _repo.ListComments(id);

        [HttpPost("submissions/{id:guid}/comments")]
        public Task<Response> AddComment(Guid id, [FromBody] NoteRequest req)
            => _repo.AddComment(id, req?.Content ?? "", req?.ByRole ?? "Executor");
    }

    public class NoteRequest
    {
        public string? Content { get; set; }
        public string? ByRole { get; set; }
    }
}

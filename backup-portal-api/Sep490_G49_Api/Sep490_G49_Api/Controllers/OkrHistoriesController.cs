using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.OkrHistories;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OkrHistoriesController : ControllerBase
    {
        private readonly IOkrHistoryRepository okrRepository;

        public OkrHistoriesController(IOkrHistoryRepository okrRepository)
        {
            this.okrRepository = okrRepository;
        }

        [HttpGet("{okrId}")]
        public async Task<ActionResult<IEnumerable<OkrHistoryDTO>>> GetOkrHistories(Guid okrId)
        {
            var histories = await okrRepository.GetOkrHistories(okrId);

            if (histories == null)
            {
                return NotFound();
            }

            return Ok(histories);
        }
        [HttpGet("{okrId}/getComments")]
        public async Task<ActionResult<IEnumerable<OKRHistoryCommentDTO>>> GetComments(Guid okrId)
        {
            var comments = await okrRepository.GetComments(okrId);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost("{okrId}/comments")]
        [Consumes("multipart/form-data")]
        public async Task<Response> AddComment(Guid okrId, [FromForm] AddCommentRequest req)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key]?.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            Console.WriteLine($"[ModelError] {key}: {error.ErrorMessage}");
                        }
                    }
                }

                // Trả lại lỗi chi tiết
                return new Response
                {
                    Message = "ModelState is invalid",
                };
            }
            return await okrRepository.AddComment(okrId, req.Text, req.Attachments);
        }

        [HttpDelete("comments/{okrHistoryId}")]
        public async Task<Response> DeleteComment(Guid okrHistoryId)
        {
            return await okrRepository.DeleteComment(okrHistoryId);
            
        }

        [HttpGet("download")]
        public IActionResult Download([FromQuery] Guid fileId)
        {
            var info = okrRepository.GetCommentFilePathAsync(fileId).Result;
            if (info == null) return NotFound();
            return PhysicalFile(info.Value.PhysicalPath, info.Value.ContentType, info.Value.FileName, true);
        }



    }
}

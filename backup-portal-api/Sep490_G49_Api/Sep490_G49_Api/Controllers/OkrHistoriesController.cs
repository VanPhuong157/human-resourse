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
        public async Task<Response> AddComment(Guid okrId, [FromBody] string comment)
        {
            return await okrRepository.AddComment(okrId, comment);
        }

        [HttpDelete("comments/{okrHistoryId}")]
        public async Task<Response> DeleteComment(Guid okrHistoryId)
        {
            return await okrRepository.DeleteComment(okrHistoryId);
            
        }

    }
}

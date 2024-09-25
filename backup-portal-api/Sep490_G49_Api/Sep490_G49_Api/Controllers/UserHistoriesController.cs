using BusinessObjects.DTO.UserInformation;
using Microsoft.AspNetCore.Mvc;
using Repository.UserHistories;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHistoriesController : ControllerBase
    {
        private readonly IUserHistoryRepository _repository;
        public UserHistoriesController(IUserHistoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserHistoryResponseDTO>>> GetUserHistories(Guid userId)
        {
            var histories = await _repository.GetUserHistories(userId);

            if (histories == null)
            {
                return NotFound();
            }

            return Ok(histories);
        }
    }
}

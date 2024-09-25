using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Objectives;

namespace Sep490_G49_Api.Controllers.Worker
{
    [Route("api/worker/[controller]")]
    [ApiController]
    public class OkrsController : ControllerBase
    {
        private readonly IOkrRepository okrRepository;

        public OkrsController(IOkrRepository okrRepository)
        {
            this.okrRepository = okrRepository;
        }

        [HttpGet]
        public Task<Response> GetOkrsSlowProgress()
        {
            Task<Response> okrsSlowProgress = okrRepository.GetOkrsSlowProgress();
            return okrsSlowProgress;
        }
    }
}

using BusinessObjects.DTO.HomePageReasonDTO;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.HomePageReasons;
using System.Threading.Tasks;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageReasonsController : ControllerBase
    {
        private readonly IHomePageReasonRepository _reasonRepository;

        public HomePageReasonsController(IHomePageReasonRepository reasonRepository)
        {
            _reasonRepository = reasonRepository;
        }

        [HttpPost]
        public async Task<Response> CreateReason(ReasonCreateDTO reasonCreateDTO)
        {
            return await _reasonRepository.CreateReason(reasonCreateDTO);
        }

        [HttpGet]
        public async Task<PaginatedList<ReasonDTO>> GetAllReasons(int pageIndex = 1, int pageSize = 10)
        {
            return await _reasonRepository.GetAllReasons(pageIndex, pageSize);
        }

        [HttpPut("{id}")]
        public async Task<Response> UpdateReason(int id, ReasonCreateDTO reasonCreateDTO)
        {
            return await _reasonRepository.UpdateReason(id, reasonCreateDTO);
        }

        [HttpDelete("{id}")]
        public async Task<Response> DeleteReason(int id)
        {
            return await _reasonRepository.DeleteReason(id);
        }
    }
}


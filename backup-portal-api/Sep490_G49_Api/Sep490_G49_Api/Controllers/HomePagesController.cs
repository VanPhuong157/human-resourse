using BusinessObjects.DTO.HomePage;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.HomePages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.HomePages;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePagesController : ControllerBase
    {
        private readonly IHomePageRepository _homePageRepository;

        public HomePagesController(IHomePageRepository homePageRepository)
        {
            _homePageRepository = homePageRepository;
        }

        [HttpGet]
        public async Task<PaginatedList<HomePageDTO>> GetAllHomePages(
    int pageIndex = 1,
    int pageSize = 10,
    DateTime? startTime = null,
    DateTime? endTime = null,
    int? version = null, string? status = null)
        {
            return await _homePageRepository.GetAllHomePages(pageIndex, pageSize, startTime, endTime, version,status);
        }

        [HttpPost]
        public async Task<Response> CreateHomePage([FromForm] HomePageCreateDTO homePageCreateDTO)
        {
            return await _homePageRepository.CreateHomePage(homePageCreateDTO);
        }

        [HttpGet("GetLatestVersion")]
        public async Task<HomePageLastestDTO> GetLastestVersion()
        {
            return await _homePageRepository.GetLastestVersion();
        }
        [HttpGet("GetHomePageActive")]
        public async Task<HomePageDTO> GetHomePageActive()
        {

            return await _homePageRepository.GetHomePageActive();
        }


        [HttpPut("{id}/Activate")]
        public async Task<Response> ActivateHomePage(int id)
        {
            return await _homePageRepository.ActivateHomePage(id);
        }

        [HttpPut("{id}")]
        public async Task<Response> UpdateStatus(int id, [FromBody] HomePageEditDTO homePageEditDTO)
        {
            return await _homePageRepository.UpdateStatus(id, homePageEditDTO);
        }

        [HttpGet("download-image/{id}")]
        public async Task<IActionResult> GetBackgroundImage(int id)
        {
            
            var homepage = await _homePageRepository.GetBackgroundImageByIdAsync(id);

            if (homepage == null || string.IsNullOrEmpty(homepage.ImageBackgroundDetail))
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "CV not found." });
            }

            try
            {
                var fileBytes = await _homePageRepository.GetBackgroundFileAsync(homepage.ImageBackgroundPath);
                return File(fileBytes, "image/jpg", $"{homepage.Title}.png");
            }
            catch (FileNotFoundException)
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "Image file not found on server." });
            }
        }
    }
}
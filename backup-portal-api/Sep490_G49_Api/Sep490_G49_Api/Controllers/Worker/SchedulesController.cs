using BusinessObjects.DTO.Schedule;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Schedules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sep490_G49_Api.Controllers.Worker
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;

        public SchedulesController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        public async Task<PaginatedList<ScheduleDTO>> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            return await _scheduleRepository.GetAll(pageIndex, pageSize);
        }

        [HttpPost]
        public async Task<Response> Create([FromForm] CreateScheduleDTO createSchedule)
        {
            return await _scheduleRepository.Create(createSchedule);
        }

        [HttpPut("{id}/approve")]
        public async Task<Response> Approve(Guid id, [FromBody] ApproveScheduleDTO approve)
        {
            return await _scheduleRepository.Approve(id, approve);
        }

        [HttpGet("file/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var response = await _scheduleRepository.DownloadFile(fileName);
            if (response.Code != ResponseCode.Success)
            {
                return NotFound(response.Message);
            }

            var fileBytes = response.Data as byte[];
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}

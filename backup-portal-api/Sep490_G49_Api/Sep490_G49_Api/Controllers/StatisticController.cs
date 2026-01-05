using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Statistic;

namespace API.Controllers
{
    [Route("api/statistic")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticRepository _statisticRepo;

        public StatisticController(IStatisticRepository statisticRepo)
        {
            _statisticRepo = statisticRepo;
        }

        // Dashboard OKR
        [HttpGet("work-dashboard/okr")]
        public async Task<IActionResult> GetOkrWorkDashboard(
        [FromQuery] Guid? departmentId,
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
        {
            var data = await _statisticRepo.GetOkrWorkDashboardAsync(
                departmentId, userId, from, to);
            return Ok(data);
        }

        // Dashboard PolicyStep (Project Task)
        [HttpGet("work-dashboard/policystep")]
        public async Task<IActionResult> GetPolicyStepWorkDashboard(
            [FromQuery] Guid? departmentId,
            [FromQuery] Guid? userId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var data = await _statisticRepo.GetPolicyStepWorkDashboardAsync(
                departmentId, userId, from, to);
            return Ok(data);
        }
    }
}

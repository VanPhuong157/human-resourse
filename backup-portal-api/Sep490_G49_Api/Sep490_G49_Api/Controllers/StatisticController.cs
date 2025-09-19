using BusinessObjects.DTO.Statistic;
using Microsoft.AspNetCore.Mvc;
using Repository.Statistic;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private IStatisticRepository _statistic;
        public StatisticController(IStatisticRepository statistic)
        {
            _statistic = statistic;
        }

        [HttpGet("users/by-department")]
        public async Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId)
        {
            return await _statistic.UsersStatistic(departmentId);
        }

        [HttpGet("okr/by-department")]
        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            return _statistic.GetOkrStatisticsByStatus(departmentId);
        }
        [HttpGet("okr-request/by-department")]
        public IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId)
        {
            return _statistic.GetOkrStatisticsByApproveStatus(departmentId);
        }
    }
}

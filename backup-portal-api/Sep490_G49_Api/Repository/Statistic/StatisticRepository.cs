using BusinessObjects.DTO.Statistic;

using Repository.Objectives;
using Repository.Users;

namespace Repository.Statistic
{
    public class StatisticRepository : IStatisticRepository
    {
        private IOkrRepository _okrDAO;
        private IUserRepository _userDAO;

        public StatisticRepository(IOkrRepository okrDAO, IUserRepository userDAO)
        {
            _okrDAO = okrDAO;
            _userDAO = userDAO;
        }


        public IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByApproveStatus(departmentId);
        }

        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByStatus(departmentId);
        }


        public async Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId)
        {
            var total = _userDAO.GetTotalUserCount(departmentId);
            var percent = _userDAO.CalculateUserGrowthPercentage(departmentId);
            return new TotalStatisticDTO
            {
                Total = total,
                Percentage = percent
            };
        }
    }
}

using BusinessObjects.DTO.Role;
using BusinessObjects.DTO.Statistic;

namespace Repository.Statistic
{
    public interface IStatisticRepository
    {
        IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId);
        IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId);
        Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId);

    }
}

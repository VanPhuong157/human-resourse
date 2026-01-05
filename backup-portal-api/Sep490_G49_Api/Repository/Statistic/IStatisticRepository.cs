using BusinessObjects.DTO.Role;
using BusinessObjects.DTO.Statistic;

namespace Repository.Statistic
{
    public interface IStatisticRepository
    {
        IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId);
        IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId);
        Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId);

        // Dashboard OKR
        Task<WorkDashboardDTO> GetOkrWorkDashboardAsync(
         Guid? departmentId, Guid? userId, DateTime? from, DateTime? to);

        Task<WorkDashboardDTO> GetPolicyStepWorkDashboardAsync(
            Guid? departmentId, Guid? userId, DateTime? from, DateTime? to);
    }
}

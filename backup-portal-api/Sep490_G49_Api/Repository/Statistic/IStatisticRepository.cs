using BusinessObjects.DTO.Role;
using BusinessObjects.DTO.Statistic;

namespace Repository.Statistic
{
    public interface IStatisticRepository
    {
        Task<TotalStatisticDTO> CandidateStatistic(Guid? departmentId);
        IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId);
        IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId);
        List<JobPostCandidateCountDTO> GetCandidateCountByJobPost(Guid? departmentId);
        Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId);
        Task<TotalStatisticDTO> JobPostsStatistic(Guid? departmentId);

    }
}

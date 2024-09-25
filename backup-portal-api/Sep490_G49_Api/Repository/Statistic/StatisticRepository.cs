using BusinessObjects.DTO.Statistic;
using DataAccess.Candidates;
using DataAccess.JobPosts;
using DataAccess.Okrs;
using DataAccess.Users;

namespace Repository.Statistic
{
    public class StatisticRepository : IStatisticRepository
    {
        private CandidateDAO _candidateDAO;
        private OkrDAO _okrDAO;
        private UserDAO _userDAO;
        private JobPostDAO _jobPostDAO;

        public StatisticRepository(CandidateDAO candidateDao, OkrDAO okrDAO, UserDAO userDAO, JobPostDAO jobPostDAO)
        {
            _candidateDAO = candidateDao;
            _okrDAO = okrDAO;
            _userDAO = userDAO;
            _jobPostDAO = jobPostDAO;
        }
        public async Task<TotalStatisticDTO> CandidateStatistic(Guid? departmentId)
        {
            var total = _candidateDAO.CountCandidatesNotPassedByDepartment(departmentId);
            var percent = _candidateDAO.CalculatePercentageDifferenceApplyDate(departmentId);
            return new TotalStatisticDTO
            {
                Total = total,
                Percentage = percent
            };
        }

        public List<JobPostCandidateCountDTO> GetCandidateCountByJobPost(Guid? departmentId)
        {
            return _candidateDAO.GetCandidateCountByJobPost(departmentId);
        }

        public IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByApproveStatus(departmentId);
        }

        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByStatus(departmentId);
        }

        public async Task<TotalStatisticDTO> JobPostsStatistic(Guid? departmentId)
        {
            var total = _jobPostDAO.GetTotalRecruitingJobPostCount(departmentId);
            var percent = _jobPostDAO.CalculateJobPostGrowthPercentage(departmentId);
            return new TotalStatisticDTO
            {
                Total = total,
                Percentage = percent
            };
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

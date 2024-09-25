using BusinessObjects.DTO.JobPost;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.JobPosts;

namespace Repository.JobPosts
{
    public class JobPostRepository : IJobPostRepository
    {
        private readonly JobPostDAO jobPostDAO;
        public JobPostRepository(JobPostDAO jobPostDAO)
        {
            this.jobPostDAO = jobPostDAO;
        }
        public async Task<Response> CreateJobPost(JobPostRequestDTO newJobPost)
        {
            return await jobPostDAO.CreatePost(newJobPost);
        }

        public async Task<Response> EditJobPost(Guid id, JobPostRequestDTO jobDetails)
        {
            return await jobPostDAO.EditJobPost(id, jobDetails);
        }

        public async Task<PaginatedList<JobPostDTO>> GetJobPosts(
            int pageIndex,
            int pageSize,
            string? title = null,
            int? minSalary = null,
            int? maxSalary = null,
            int? minYear = null,
            int? maxYear = null,
            string? type = null,
            string? status = null,
            Guid? departmentId = null)
        {
            return await jobPostDAO.GetJobPosts(pageIndex, pageSize, title, minSalary, maxSalary, minYear, maxYear, type, status, departmentId);
        }

        public async Task<JobPostDTO> GetJobPostById(Guid id)
        {
            return await jobPostDAO.GetJobPostById(id);
        }

    }
}

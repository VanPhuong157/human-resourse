using BusinessObjects.DTO;
using BusinessObjects.DTO.JobPost;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.JobPosts
{
    public interface IJobPostRepository
    {
        Task<JobPostDTO> GetJobPostById(Guid id);
        Task<Response> EditJobPost(Guid id, JobPostRequestDTO jobDetails);
        Task<Response> CreateJobPost(JobPostRequestDTO newJobPost);
        Task<PaginatedList<JobPostDTO>> GetJobPosts(
            int pageIndex,
            int pageSize,
            string? title = null,
            int? minSalary = null,
            int? maxSalary = null,
            int? minYear = null,
            int? maxYear = null,
            string? type = null,
            string? status = null,
            Guid? departmentId = null);
    }
}

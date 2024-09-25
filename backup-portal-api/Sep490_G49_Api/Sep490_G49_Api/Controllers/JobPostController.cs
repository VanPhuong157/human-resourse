using BusinessObjects.DTO.JobPost;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.JobPosts;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostController : ControllerBase
    {
        private readonly IJobPostRepository jobPostRepository;
        public JobPostController(IJobPostRepository jobPostRepository)
        {
            this.jobPostRepository = jobPostRepository;
        }
        [HttpGet]
        public async Task<PaginatedList<JobPostDTO>> GetPosts(
            int pageIndex = 1, 
            int pageSize = 10, 
            string? title = null,
            int? minSalary = null,
            int? maxSalary = null,
            int? minYear = null,
            int? maxYear = null,
            string? type = null,
            string? status = null,
            Guid? departmentId = null)
        {
            var jobs = await jobPostRepository.GetJobPosts(pageIndex, pageSize, title, minSalary, maxSalary, minYear, maxYear, type, status, departmentId);
            return jobs;
        }

        [HttpGet("{postId}")]
        public async Task<JobPostDTO> GetJobPostById(Guid postId)
        {
            return await jobPostRepository.GetJobPostById(postId);
        }
        [HttpPut("{postId}")]
        public async Task<Response> EditJobPost(Guid postId, JobPostRequestDTO jobDetails)
        {
            if (jobDetails == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }

            var existingJobPost = await jobPostRepository.GetJobPostById(postId);
            if (existingJobPost == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Job post not found" };
            }

            return await jobPostRepository.EditJobPost(postId, jobDetails);
        }
        [HttpPost]
        public async Task<Response> CreateJobPost(JobPostRequestDTO newJobPost)
        {
            if (newJobPost == null)
            {
                return new Response { Message = "Request is null." };
            }

            return await jobPostRepository.CreateJobPost(newJobPost);
        }


    }
}

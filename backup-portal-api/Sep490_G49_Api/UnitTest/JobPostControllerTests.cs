using BusinessObjects.DTO.JobPost;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Moq;
using Repository.JobPosts;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class JobPostControllerTests
    {
        private readonly Mock<IJobPostRepository> _jobPostRepositoryMock;
        private readonly JobPostController _jobPostController;

        public JobPostControllerTests()
        {
            _jobPostRepositoryMock = new Mock<IJobPostRepository>();
            _jobPostController = new JobPostController(_jobPostRepositoryMock.Object);
        }

        [Fact]
        public async Task GetJobPostById_ShouldReturnJobPostDTO()
        {
            // Arrange
            var jobPostId = Guid.NewGuid();
            var jobPost = new JobPostDTO { Id = jobPostId, Title = "Developer" };

            _jobPostRepositoryMock
                .Setup(repo => repo.GetJobPostById(jobPostId))
                .ReturnsAsync(jobPost);

            // Act
            var result = await _jobPostController.GetJobPostById(jobPostId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Developer", result.Title);
        }

        [Fact]
        public async Task EditJobPost_ShouldReturnSuccessResponse()
        {
            // Arrange
            var jobPostId = Guid.NewGuid();
            var jobDetails = new JobPostRequestDTO
            {
                Title = "Updated Title",
                Department = Guid.NewGuid(),
                NumberOfRecruits = 5,
                Benefits = "Benefits",
                Requirements = "Requirements",
                ExperienceYear = 2,
                Description = "Description",
                Type = "Full-time",
                Status = "Open",
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Salary = 5000
            };

            var existingJobPost = new JobPostDTO
            {
                Id = jobPostId,
                Title = "Old Title",
                DepartmentId = jobDetails.Department,
                NumberOfRecruits = jobDetails.NumberOfRecruits,
                Benefits = jobDetails.Benefits,
                Requirements = jobDetails.Requirements,
                ExperienceYear = jobDetails.ExperienceYear,
                Description = jobDetails.Description,
                Type = jobDetails.Type,
                Status = jobDetails.Status,
                ExpiryDate = jobDetails.ExpiryDate,
                Salary = jobDetails.Salary
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };

            _jobPostRepositoryMock
                .Setup(repo => repo.GetJobPostById(jobPostId))
                .ReturnsAsync(existingJobPost);

            _jobPostRepositoryMock
                .Setup(repo => repo.EditJobPost(jobPostId, jobDetails))
                .ReturnsAsync(response);

            // Act
            var result = await _jobPostController.EditJobPost(jobPostId, jobDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.Success, result.Code);
            Assert.Equal("Edit successfully!", result.Message);
        }


        [Fact]
        public async Task CreateJobPost_ShouldReturnSuccessResponse()
        {
            // Arrange
            var jobDetails = new JobPostRequestDTO
            {
                Title = "New Job",
                Department = Guid.NewGuid(),
                NumberOfRecruits = 5,
                Benefits = "Benefits",
                Requirements = "Requirements",
                ExperienceYear = 2,
                Description = "Description",
                Type = "Full-time",
                Status = "Open",
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Salary = 5000
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Create successfully!" };

            _jobPostRepositoryMock
                .Setup(repo => repo.CreateJobPost(jobDetails))
                .ReturnsAsync(response);

            // Act
            var result = await _jobPostController.CreateJobPost(jobDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create successfully!", result.Message);
        }

        [Fact]
        public async Task CreateJobPost_ShouldReturnErrorResponse_WhenRequestIsNull()
        {
            // Arrange
            JobPostRequestDTO jobDetails = null;

            // Act
            var result = await _jobPostController.CreateJobPost(jobDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Request is null.", result.Message);
        }

        [Fact]
        public async Task EditJobPost_ShouldReturnNotFoundResponseWhenJobPostNotFound()
        {
            // Arrange
            var jobPostId = Guid.NewGuid();
            var jobDetails = new JobPostRequestDTO
            {
                Title = "Updated Title",
                Department = Guid.NewGuid(),
                NumberOfRecruits = 5,
                Benefits = "Benefits",
                Requirements = "Requirements",
                ExperienceYear = 2,
                Description = "Description",
                Type = "Full-time",
                Status = "Open",
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Salary = 5000
            };

            _jobPostRepositoryMock
                .Setup(repo => repo.GetJobPostById(jobPostId))
                .ReturnsAsync((JobPostDTO)null);

            // Act
            var result = await _jobPostController.EditJobPost(jobPostId, jobDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.NotFound, result.Code);
            Assert.Equal("Job post not found", result.Message);
        }

        [Fact]
        public async Task EditJobPost_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            var jobPostId = Guid.NewGuid();
            JobPostRequestDTO jobDetails = null;

            // Act
            var result = await _jobPostController.EditJobPost(jobPostId, jobDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }
    }
}

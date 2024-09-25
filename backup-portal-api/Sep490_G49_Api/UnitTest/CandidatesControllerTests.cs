using BusinessObjects.DTO.Candidate;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Moq;
using Repository.Candidates;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class CandidatesControllerTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly CandidatesController _candidatesController;

        public CandidatesControllerTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _candidatesController = new CandidatesController(_candidateRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCandidates_ShouldReturnPaginatedList()
        {
            // Arrange
            var paginatedList = new PaginatedList<CandidateResponseDTO>(
                new List<CandidateResponseDTO> { new CandidateResponseDTO { FullName = "John Doe" } },
                1, 1, 1
            );

            _candidateRepositoryMock
                .Setup(repo => repo.GetCandidates(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string?>(),
                    It.IsAny<string?>(),
                    It.IsAny<Guid?>()
                ))
                .ReturnsAsync(paginatedList);

            // Act
            var result = await _candidatesController.GetCandidates();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal("John Doe", result.Items[0].FullName);
        }

        [Fact]
        public async Task CreateCandidate_ShouldReturnSuccessResponse()
        {
            // Arrange
            var candidateDTO = new CandidateDTO
            {
                FullName = "Hung NE",
                Email = "hungne@gmail.com",
                PhoneNumber = "0352278897",
                CvFile = new FormFile(null, 0, 0, "CV.pdf", "CV.pdf")
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Create successfully!" };

            _candidateRepositoryMock
                .Setup(repo => repo.CreateCandidate(candidateDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _candidatesController.CreateCandidate(candidateDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create successfully!", result.Message);
        }

        [Fact]
        public async Task EditStatus_ShouldReturnSuccessResponse()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var candidateUpdateStatusDTO = new CandidateUpdateStatusDTO
            {
                NewStatus = "Pass"
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Edit status successfully!" };

            _candidateRepositoryMock
                .Setup(repo => repo.EditStatus(candidateId, candidateUpdateStatusDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _candidatesController.EditStatus(candidateId, candidateUpdateStatusDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Edit status successfully!", result.Message);
        }

        [Fact]
        public async Task GetCvDetail_ShouldReturnCvString()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var cvContent = "This is a CV content";

            _candidateRepositoryMock
                .Setup(repo => repo.GetCvDetailById(candidateId))
                .ReturnsAsync(cvContent);

            // Act
            var result = await _candidatesController.GetCvDetail(candidateId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cvContent, result);
        }
        [Fact]
        public async Task CreateCandidate_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            CandidateDTO candidateDTO = null;

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };

            // Act
            var result = await _candidatesController.CreateCandidate(candidateDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }

        [Fact]
        public async Task EditStatus_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            CandidateUpdateStatusDTO candidateUpdateStatusDTO = null;

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };

            // Act
            var result = await _candidatesController.EditStatus(candidateId, candidateUpdateStatusDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }

        [Fact]
        public async Task EditStatus_ShouldReturnBadRequestResponseWhenCandidateNotFound()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var candidateUpdateStatusDTO = new CandidateUpdateStatusDTO
            {
                NewStatus = "Pass"
            };

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Candidate not found" };

            _candidateRepositoryMock
                .Setup(repo => repo.EditStatus(candidateId, candidateUpdateStatusDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _candidatesController.EditStatus(candidateId, candidateUpdateStatusDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Candidate not found", result.Message);
        }
    }
}

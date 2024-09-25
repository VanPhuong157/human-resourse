using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.Objectives;

namespace UnitTest
{
    public class OkrsControllerTests
    {
        private readonly Mock<IOkrRepository> _okrRepositoryMock;
        private readonly OkrsController _okrsController;

        public OkrsControllerTests()
        {
            _okrRepositoryMock = new Mock<IOkrRepository>();
            _okrsController = new OkrsController(_okrRepositoryMock.Object);
        }

        [Fact]
        public async Task GetObkrById_ShouldReturnOKRDetailsDTO()
        {
            // Arrange
            var okrId = Guid.NewGuid();
            var okrDetails = new OKRDetailsDTO { Id = okrId, Title = "OKR Title" };

            _okrRepositoryMock
                .Setup(repo => repo.GetOkrById(okrId))
                .ReturnsAsync(okrDetails);

            // Act
            var result = await _okrsController.GetOkrById(okrId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OKR Title", result.Title);
        }

        [Fact]
        public async Task CreateObjective_ShouldReturnSuccessResponse()
        {
            // Arrange
            var okrCreateDTO = new OKRCreateDTO
            {
                Title = "New OKR",
                Type = "Objective",
                Scope = "Scope",
                DepartmentId = Guid.NewGuid(),
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Create successfully!" };

            _okrRepositoryMock
                .Setup(repo => repo.CreateOkr(okrCreateDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _okrsController.CreateOkr(okrCreateDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create successfully!", result.Message);
        }

        [Fact]
        public async Task CreateObjective_ShouldReturnErrorResponse_WhenRequestIsNull()
        {
            // Arrange
            OKRCreateDTO okrCreateDTO = null;

            // Act
            var result = await _okrsController.CreateOkr(okrCreateDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Request is null.", result.Message);
        }

        [Fact]
        public async Task GetOkrsByDepartmentId_ShouldReturnPaginatedList()
        {
            // Arrange
            var paginatedList = new PaginatedList<OKRDTO>(
                new List<OKRDTO> { new OKRDTO { Title = "OKR Title" } },
                1, 1, 1
            );

            _okrRepositoryMock
                .Setup(repo => repo.GetOkrsByDepartmentId(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid?>()
                ))
                .ReturnsAsync(paginatedList);

            // Act
            var result = await _okrsController.GetOkrsByDepartmentId();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal("OKR Title", result.Items[0].Title);
        }

        [Fact]
        public async Task GetOkrsForRequests_ShouldReturnPaginatedList()
        {
            // Arrange
            var paginatedList = new PaginatedList<OKRRequestDTO>(
                new List<OKRRequestDTO> { new OKRRequestDTO { Title = "OKR Title" } },
                1, 1, 1
            );

            _okrRepositoryMock
                .Setup(repo => repo.GetOkrsRequests(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid?>()
                ))
                .ReturnsAsync(paginatedList);

            // Act
            var result = await _okrsController.GetOkrsRequests();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal("OKR Title", result.Items[0].Title);
        }

        [Fact]
        public async Task UpdateApproveStatus_ShouldReturnOkResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ApproveStatusUpdateDTO { ApproveStatus = "Approved" };
            var response = new Response { Code = ResponseCode.Success, Message = "Approve status updated successfully!" };

            _okrRepositoryMock
                .Setup(repo => repo.UpdateApproveStatus(id, dto))
                .ReturnsAsync(response);

            // Act
            var result = await _okrsController.UpdateApproveStatus(id, dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task UpdateApproveStatus_ShouldReturnNotFound_WhenResponseIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ApproveStatusUpdateDTO { ApproveStatus = "Approved" };

            _okrRepositoryMock
                .Setup(repo => repo.UpdateApproveStatus(id, dto))
                .ReturnsAsync((Response)null);

            // Act
            var result = await _okrsController.UpdateApproveStatus(id, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

using BusinessObjects.DTO.UserInformation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.UserHistories;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class UserHistoriesControllerTests
    {
        private readonly Mock<IUserHistoryRepository> _mockRepository;
        private readonly UserHistoriesController _controller;

        public UserHistoriesControllerTests()
        {
            _mockRepository = new Mock<IUserHistoryRepository>();
            _controller = new UserHistoriesController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetUserHistories_ReturnsCorrectUserHistories()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUserHistories = new List<UserHistoryResponseDTO>
            {
                new UserHistoryResponseDTO
                {
                    UserId = userId,
                    RoleName = "Admin",
                    DepartmentName = "IT",
                    StartTime = DateTime.UtcNow.AddHours(-2),
                    EndTime = DateTime.UtcNow.AddHours(-1)
                },
                new UserHistoryResponseDTO
                {
                    UserId = userId,
                    RoleName = "User",
                    DepartmentName = "HR",
                    StartTime = DateTime.UtcNow.AddHours(-4),
                    EndTime = DateTime.UtcNow.AddHours(-3)
                }
            };

            // Setup mock repository method
            _mockRepository.Setup(repo => repo.GetUserHistories(userId))
                           .ReturnsAsync(expectedUserHistories);

            // Act
            var result = await _controller.GetUserHistories(userId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<UserHistoryResponseDTO>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedHistories = Assert.IsAssignableFrom<IEnumerable<UserHistoryResponseDTO>>(returnValue.Value);
            Assert.Equal(expectedUserHistories.Count, returnedHistories.Count());
        }

        [Fact]
        public async Task GetUserHistories_ReturnsNotFoundWhenNoHistories()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Setup mock repository method to return null (no histories found)
            _mockRepository.Setup(repo => repo.GetUserHistories(userId))
                           .ReturnsAsync((List<UserHistoryResponseDTO>)null);

            // Act
            var result = await _controller.GetUserHistories(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.OkrHistories;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class OkrHistoriesControllerTests
    {
        private readonly Mock<IOkrHistoryRepository> _okrRepositoryMock;
        private readonly OkrHistoriesController _okrHistoriesController;

        public OkrHistoriesControllerTests()
        {
            _okrRepositoryMock = new Mock<IOkrHistoryRepository>();
            _okrHistoriesController = new OkrHistoriesController(_okrRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOkrHistories_ReturnsOkObjectResult_WhenHistoriesExist()
        {
            // Arrange
            Guid okrId = Guid.NewGuid();
            var mockHistories = new List<OkrHistoryDTO>
            {
                new OkrHistoryDTO { OkrId = okrId, CreatedAt = DateTime.UtcNow, OldProgress = 50, NewProgress = 75 },
                new OkrHistoryDTO { OkrId = okrId, CreatedAt = DateTime.UtcNow.AddDays(-1), OldProgress = 40, NewProgress = 60 }
            };

            _okrRepositoryMock
                .Setup(repo => repo.GetOkrHistories(okrId))
                .ReturnsAsync(mockHistories);

            // Act
            var result = await _okrHistoriesController.GetOkrHistories(okrId);

            // Assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<OkrHistoryDTO>>(okObjectResult.Value);
            Assert.Equal(2, model.Count()); 
        }

        [Fact]
        public async Task GetOkrHistories_ReturnsNotFoundResult_WhenHistoriesDoNotExist()
        {
            // Arrange
            Guid okrId = Guid.NewGuid();

            _okrRepositoryMock
                .Setup(repo => repo.GetOkrHistories(okrId))
                .ReturnsAsync((List<OkrHistoryDTO>)null); // Trả về null để simulates không tìm thấy bản ghi nào

            // Act
            var result = await _okrHistoriesController.GetOkrHistories(okrId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

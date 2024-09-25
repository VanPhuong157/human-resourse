using BusinessObjects.DTO.Department;
using BusinessObjects.Response;
using Moq;
using Repository.Departments;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class DepartmentControllerTests
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly DepartmentsController _departmentsController;

        public DepartmentControllerTests()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _departmentsController = new DepartmentsController(_departmentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateDepartment_ShouldReturnSuccessResponse()
        {
            // Arrange
            var departmentDTO = new CreateDepartmentDTO
            {
                DepartmentName = "Finance"
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Create successfully!" };

            _departmentRepositoryMock
                .Setup(repo => repo.CreateDepartment(departmentDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _departmentsController.CreateDepartment(departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create successfully!", result.Message);
        }

        [Fact]
        public async Task EditDepartment_ShouldReturnSuccessResponse()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var departmentDTO = new CreateDepartmentDTO
            {
                DepartmentName = "Finance"
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };

            _departmentRepositoryMock
                .Setup(repo => repo.UpdateDepartment(departmentId, departmentDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _departmentsController.EditDepartment(departmentId, departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Edit successfully!", result.Message);
        }

        [Fact]
        public async Task EditDepartment_ShouldReturnBadRequestResponseWhenDepartmentNotFound()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var departmentDTO = new CreateDepartmentDTO
            {
                DepartmentName = "Finance"
            };

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Department not found" };

            _departmentRepositoryMock
                .Setup(repo => repo.UpdateDepartment(departmentId, departmentDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _departmentsController.EditDepartment(departmentId, departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Department not found", result.Message);
        }

        [Fact]
        public async Task CreateDepartment_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            CreateDepartmentDTO departmentDTO = null;

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };

            // Act
            var result = await _departmentsController.CreateDepartment(departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }

        [Fact]
        public async Task EditDepartment_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            CreateDepartmentDTO departmentDTO = null;

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };

            // Act
            var result = await _departmentsController.EditDepartment(departmentId, departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }
    }
}

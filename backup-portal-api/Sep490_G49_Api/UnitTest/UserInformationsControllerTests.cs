using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.UserInformations;
using Sep490_G49_Api.Controllers;

namespace UnitTest
{
    public class UserInformationsControllerTests
    {
        private readonly Mock<IUserInformationRepository> _mockRepo;
        private readonly UserInformationsController _controller;

        public UserInformationsControllerTests()
        {
            _mockRepo = new Mock<IUserInformationRepository>();
            _controller = new UserInformationsController(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateUserDetails_ReturnsCreatedUserDetails()
        {
            // Arrange
            var userDetailsDto = new UserDetailsDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "User details created successfully." };

            _mockRepo.Setup(repo => repo.SaveUserDetails(It.IsAny<UserDetailsDTO>())).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateUserDetails(userDetailsDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Response>(okResult.Value);
            Assert.Equal(response.Message, returnValue.Message);
        }

        [Fact]
        public async Task EditUserDetails_ReturnsUpdatedUserDetails()
        {
            // Arrange
            var userDetailsDto = new UserDetailsDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "User details updated successfully." };

            _mockRepo.Setup(repo => repo.SaveUserDetails(It.IsAny<UserDetailsDTO>())).ReturnsAsync(response);

            // Act
            var result = await _controller.EditUserDetails(userDetailsDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Response>(okResult.Value);
            Assert.Equal(response.Message, returnValue.Message);
        }

        [Fact]
        public async Task EditPersonalProfile_ReturnsUpdatedPersonalProfile()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updatePersonalProfileDTO = new UpdatePersonalProfileDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "Personal profile updated successfully." };

            _mockRepo.Setup(repo => repo.EditPersonalProfile(userId, It.IsAny<UpdatePersonalProfileDTO>())).ReturnsAsync(response);

            // Act
            var result = await _controller.EditPersonalProfile(userId, updatePersonalProfileDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Response>(okResult.Value);
            Assert.Equal(response.Message, returnValue.Message);
        }

        [Fact]
        public async Task GetUsers_ReturnsPaginatedListOfUsers()
        {
            // Arrange
            var userList = new PaginatedList<UserDetailsWithoutFamilyDTO>(new List<UserDetailsWithoutFamilyDTO>(), 1, 1, 1);

            _mockRepo.Setup(repo => repo.GetUserInformations(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(userList);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            Assert.IsType<PaginatedList<UserDetailsWithoutFamilyDTO>>(result);
        }

        [Fact]
        public async Task GetPersonalByUserIdAsync_ReturnsUserDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDetails = new PersonalDetailDTO();

            _mockRepo.Setup(repo => repo.GetPersonalByUserId(userId)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.GetPersonalByUserId(userId);

            // Assert
            Assert.IsType<PersonalDetailDTO>(result);
        }

        [Fact]
        public async Task GetById_ReturnsUserDetails()
        {
            // Arrange
            var userInformationId = Guid.NewGuid();
            var userDetails = new UserDetailsWithoutFamilyDTO();

            _mockRepo.Setup(repo => repo.GetById(userInformationId)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.GetById(userInformationId);

            // Assert
            Assert.IsType<UserDetailsWithoutFamilyDTO>(result);
        }

        [Fact]
        public async Task GetByUserId_ReturnsUserDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDetails = new UserDetailsWithoutFamilyDTO();

            _mockRepo.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.GetByUserId(userId);

            // Assert
            Assert.IsType<UserDetailsWithoutFamilyDTO>(result);
        }

        [Fact]
        public async Task GetUserFamily_ReturnsUserFamilyDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userFamily = new UserFamilyDTO();

            _mockRepo.Setup(repo => repo.GetUserFamily(userId)).ReturnsAsync(userFamily);

            // Act
            var result = await _controller.GetUserFamily(userId);

            // Assert
            Assert.IsType<UserFamilyDTO>(result);
        }

        [Fact]
        public async Task CreateFamilyMember_ReturnsResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var familyDto = new CreateUpdateFamilyDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "Family member created successfully." };

            _mockRepo.Setup(repo => repo.CreateFamilyMember(userId, familyDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateFamilyMember(userId, familyDto);

            // Assert
            Assert.IsType<Response>(result);
            Assert.Equal(response.Message, result.Message);
        }

        [Fact]
        public async Task EditFamilyMember_ReturnsResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var familyDto = new CreateUpdateFamilyDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "Family member updated successfully." };

            _mockRepo.Setup(repo => repo.EditFamilyMember(userId, memberId, familyDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.EditFamilyMember(userId, memberId, familyDto);

            // Assert
            Assert.IsType<Response>(result);
            Assert.Equal(response.Message, result.Message);
        }

        [Fact]
        public async Task EditStatusAsync_ReturnsResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var editStatusDTO = new EditStatusDTO();
            var response = new Response { Code = ResponseCode.Success, Message = "Status updated successfully." };

            _mockRepo.Setup(repo => repo.EditStatus(userId, editStatusDTO)).ReturnsAsync(response);

            // Act
            var result = await _controller.EditStatus(userId, editStatusDTO);

            // Assert
            Assert.IsType<Response>(result);
            Assert.Equal(response.Message, result.Message);
        }
    }
}

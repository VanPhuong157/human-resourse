using BusinessObjects.DTO.User;
using BusinessObjects.Response;
using Moq;
using Repository.Users;
using API.Controllers;
using BusinessObjects.DTO.BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UsersController _usersController;

        public UsersControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _usersController = new UsersController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnSuccessResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var changePasswordDTO = new ChangePasswordDTO
            {
                OldPassword = "oldpassword",
                NewPassword = "newpassword"
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Password changed successfully!" };

            _userRepositoryMock
                .Setup(repo => repo.ChangePassword(userId, changePasswordDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.ChangePassword(userId, changePasswordDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Password changed successfully!", result.Message);
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnToken()
        {
            // Arrange
            var token = "fake_refresh_token";
            var refreshedToken = new Token { AccessToken = "new_fake_access_token", RefreshToken = "new_fake_refresh_token" };

            _userRepositoryMock
                .Setup(repo => repo.RefreshToken(token))
                .ReturnsAsync(refreshedToken);

            // Act
            var result = await _usersController.RefreshToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("new_fake_access_token", result.AccessToken);
            Assert.Equal("new_fake_refresh_token", result.RefreshToken);
        }

        [Fact]
        public async Task UpdateRoleDepartment_ShouldReturnSuccessResponse()
        {
            // Arrange
            var updateRoleDTO = new UpdateRoleDTO
            {
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid()
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Role and department updated successfully!" };

            _userRepositoryMock
                .Setup(repo => repo.UpdateRoleDepartment(updateRoleDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.UpdateRoleDepartment(updateRoleDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Role and department updated successfully!", result.Message);
        }

        [Fact]
        public async Task Create_ShouldReturnSuccessResponse()
        {
            // Arrange
            var newUser = new NewUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                FullName = "Test User",
                TypeOfWork = "Developer",
                DepartmentId = Guid.NewGuid(),
                RoleId = Guid.NewGuid()
            };

            var response = new Response { Code = ResponseCode.Success, Message = "User created successfully!" };

            _userRepositoryMock
                .Setup(repo => repo.Create(newUser))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.Create(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User created successfully!", result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccessResponse()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Username = "testuser",
                Password = "password"
            };

            var response = new Response { Code = ResponseCode.Success, Message = "Login successful!" };

            _userRepositoryMock
                .Setup(repo => repo.Login(loginDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.Login(loginDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login successful!", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_ShouldReturnSuccessResponse()
        {
            // Arrange
            var email = "test@example.com";

            var response = new Response { Code = ResponseCode.Success, Message = "Temporary password sent to your email!" };

            _userRepositoryMock
                .Setup(repo => repo.ForgotPassword(email))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.ForgotPassword(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Temporary password sent to your email!", result.Message);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequestWhenUserNameTooLong()
        {
            // Arrange
            var newUser = new NewUserDTO
            {
                UserName = "ausernamethatistoolongtobeacceptedbythesystemandwillcausevalidationerror",
                Email = "test@example.com",
                FullName = "Test User",
                TypeOfWork = "Developer",
                DepartmentId = Guid.NewGuid(),
                RoleId = Guid.NewGuid()
            };

            var response = new Response { Code = ResponseCode.BadRequest, Message = "UserName can't be longer than 50 characters" };

            _userRepositoryMock
                .Setup(repo => repo.Create(newUser))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.Create(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UserName can't be longer than 50 characters", result.Message);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
        }


        [Fact]
        public async Task ChangePassword_ShouldReturnUnauthorizedWhenUserNotAuthenticated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var changePasswordDTO = new ChangePasswordDTO
            {
                OldPassword = "oldpassword",
                NewPassword = "newpassword"
            };

            _userRepositoryMock
                .Setup(repo => repo.ChangePassword(userId, changePasswordDTO))
                .ReturnsAsync(new Response { Code = ResponseCode.BadRequest, Message = "User not authenticated" });

            // Act
            var result = await _usersController.ChangePassword(userId, changePasswordDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User not authenticated", result.Message);
        }

        [Fact]
        public async Task UpdateRoleDepartment_ShouldReturnNotFoundWhenUserNotFound()
        {
            // Arrange
            var updateRoleDTO = new UpdateRoleDTO
            {
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid()
            };

            _userRepositoryMock
                .Setup(repo => repo.UpdateRoleDepartment(updateRoleDTO))
                .ReturnsAsync(new Response { Code = ResponseCode.NotFound, Message = "User not found" });

            // Act
            var result = await _usersController.UpdateRoleDepartment(updateRoleDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_ShouldReturnBadRequestWhenEmailNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";

            _userRepositoryMock
                .Setup(repo => repo.ForgotPassword(email))
                .ReturnsAsync(new Response { Code = ResponseCode.NotFound, Message = "Email not found" });

            // Act
            var result = await _usersController.ForgotPassword(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Email not found", result.Message);
        }
        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenUserNameIsTaken()
        {
            // Arrange
            var newUser = new NewUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                FullName = "Test User",
                TypeOfWork = "Developer",
                DepartmentId = Guid.NewGuid(),
                RoleId = Guid.NewGuid()
            };

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Username is already taken" };

            _userRepositoryMock
                .Setup(repo => repo.Create(newUser))
                .ReturnsAsync(response);

            // Act
            var result = await _usersController.Create(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Username is already taken", result.Message);
        }
    }
}

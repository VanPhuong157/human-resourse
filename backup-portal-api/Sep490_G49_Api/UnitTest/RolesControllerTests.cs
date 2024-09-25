using AutoMapper;
using BusinessObjects.DTO.Role;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.Roles;
using Sep490_G49_Api.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class RolesControllerTests
    {
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly RolesController _rolesController;

        public RolesControllerTests()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _rolesController = new RolesController(_roleRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnSuccessResponse()
        {
            // Arrange
            var createRoleDTO = new CreateRoleDTO { Name = "Manager" };

            var response = new Response { Code = ResponseCode.Success, Message = "Create successfully!" };

            _roleRepositoryMock
                .Setup(repo => repo.CreateRole(createRoleDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _rolesController.CreateRole(createRoleDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create successfully!", result.Message);
        }

        [Fact]
        public async Task EditRole_ShouldReturnSuccessResponse()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var editRoleDTO = new CreateRoleDTO { Name = "Supervisor" };

            var response = new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };

            _roleRepositoryMock
                .Setup(repo => repo.EditRole(roleId, editRoleDTO))
                .ReturnsAsync(response);

            // Act
            var result = await _rolesController.EditRole(roleId, editRoleDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Edit successfully!", result.Message);
        }

        [Fact]
        public async Task DeleteRole_ShouldReturnSuccessResponse()
        {
            // Arrange
            var roleId = Guid.NewGuid();

            var response = new Response { Code = ResponseCode.Success, Message = "Delete successfully!" };

            _roleRepositoryMock
                .Setup(repo => repo.DeleteRole(roleId))
                .ReturnsAsync(response);

            // Act
            var result = await _rolesController.DeleteRole(roleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Delete successfully!", result.Message);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnBadRequestResponseWhenRequestIsNull()
        {
            // Arrange
            CreateRoleDTO createRoleDTO = null;

            var response = new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };

            // Act
            var result = await _rolesController.CreateRole(createRoleDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseCode.BadRequest, result.Code);
            Assert.Equal("Invalid request", result.Message);
        }
    }
}

using BusinessObjects.DTO.Permission;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.Permissions;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissonsController : ControllerBase
    {
        private readonly IPermissionRepository _permission;

        public PermissonsController(IPermissionRepository permission)
        {
            _permission = permission;
        }

        [HttpGet("get-permissions")]
        public async Task<PaginatedList<PermissionResponseDTO>> GetAllPermissions(
            string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            return await _permission.GetAllPermissions(name, pageIndex, pageSize);
        }

        [HttpGet("get-user-permissions/{userId}")]
        public async Task<PaginatedList<PermissionResponseDTO>> GetUserPermissions(
    Guid userId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            return await _permission.GetUserPermissions(userId, name, pageIndex, pageSize);
        }

        [HttpPut("edit-user-permissions/{userId}")]
        public async Task<Response> EditUserPermissions(Guid userId, [FromBody] List<Guid> permissionIds)
        {
            return await _permission.EditUserPermissions(userId, permissionIds);
        }

        [HttpGet("get-role-permissions/{roleId}")]
        public async Task<PaginatedList<PermissionResponseDTO>> GetRolePermissions(
    Guid roleId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            return await _permission.GetRolePermissions(roleId, name, pageIndex, pageSize);
        }

        [HttpPut("edit-role-permissions/{roleId}")]
        public async Task<Response> EditRolePermissions(Guid roleId, [FromBody] List<Guid> permissionIds)
        {
            return await _permission.EditRolePermissions(roleId, permissionIds);
        }
    }
}

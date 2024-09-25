using BusinessObjects.DTO.Permission;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Permissions;

namespace Repository.Permissions
{
    public class PermissionRepository : IPermissionRepository
    {
        private PermissionDAO _dao;
        public PermissionRepository(PermissionDAO dao)
        {
            _dao = dao;
        }

        public async Task<Response> EditRolePermissions(Guid roleId, List<Guid> permissionIds)
        {
            return await _dao.EditRolePermissions(roleId, permissionIds);
        }

        public async Task<Response> EditUserPermissions(Guid userId, List<Guid> permissionIds)
        {
            return await _dao.EditUserPermissions(userId, permissionIds);
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetAllPermissions(
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            return await _dao.GetAllPermissions(name, pageIndex, pageSize);
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetRolePermissions(Guid roleId, string? name = null, int pageIndex = 1, int pageSize = 10)
        {
            return await _dao.GetRolePermissions(roleId, name, pageIndex, pageSize);
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetUserPermissions(
    Guid userId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            return await _dao.GetUserPermissions(userId, name, pageIndex, pageSize);
        }
    }
}

using BusinessObjects.DTO.Permission;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.Permissions
{
    public interface IPermissionRepository
    {
        Task<PaginatedList<PermissionResponseDTO>> GetAllPermissions(
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10);
        Task<PaginatedList<PermissionResponseDTO>> GetUserPermissions(
    Guid userId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10);
        Task<Response> EditUserPermissions(Guid userId, List<Guid> permissionIds);
        Task<PaginatedList<PermissionResponseDTO>> GetRolePermissions(
    Guid roleId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10);
        Task<Response> EditRolePermissions(Guid roleId, List<Guid> permissionIds);
    }
}

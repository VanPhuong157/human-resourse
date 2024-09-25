using BusinessObjects.DTO.Role;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.Roles
{
    public interface IRoleRepository
    {
        Task<PaginatedList<RoleDTO>> GetRoles(
    int pageIndex = 1,
    int pageSize = 10,
    string? name = null,
    string? type = null);
        Task<Response> CreateRole(CreateRoleDTO createRoleDTO);
        Task<Response> EditRole(Guid id, CreateRoleDTO editRoleDTO);
        Task<Response> DeleteRole(Guid id);
    }
}

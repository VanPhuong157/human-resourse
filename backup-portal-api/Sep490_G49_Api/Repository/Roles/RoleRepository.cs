using BusinessObjects.DTO.Role;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Roles;

namespace Repository.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleDAO _dao;
        public RoleRepository(RoleDAO dao)
        {
            _dao = dao;
        }
        public async Task<Response> CreateRole(CreateRoleDTO createRoleDTO)
        {
            return await _dao.CreateRole(createRoleDTO);
        }

        public async Task<Response> DeleteRole(Guid id)
        {
            return await _dao.DeleteRole(id);
        }

        public async Task<Response> EditRole(Guid id, CreateRoleDTO editRoleDTO)
        {
            return await _dao.EditRole(id, editRoleDTO);
        }

        public async Task<PaginatedList<RoleDTO>> GetRoles(
    int pageIndex = 1,
    int pageSize = 10,
    string? name = null,
    string? type = null)
        {
            return await _dao.GetRoles(pageIndex, pageSize, name, type);
        }

    }
}

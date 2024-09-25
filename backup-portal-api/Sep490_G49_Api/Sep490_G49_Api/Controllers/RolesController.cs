using BusinessObjects.DTO.Department;
using BusinessObjects.DTO.Role;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.Roles;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<PaginatedList<RoleDTO>> GetRoles(
    int pageIndex = 1,
    int pageSize = 10,
    string? name = null,
    string? type = null)
        {
            return await _roleRepository.GetRoles(pageIndex, pageSize, name, type);
        }

        [HttpPost]
        public async Task<Response> CreateRole(CreateRoleDTO createRoleDTO)
        {
            if (createRoleDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }
            return await _roleRepository.CreateRole(createRoleDTO);
        }
        [HttpPut]
        public async Task<Response> EditRole(Guid id, CreateRoleDTO editRoleDTO)
        {
            if (editRoleDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }
            return await _roleRepository.EditRole(id, editRoleDTO);
        }
        [HttpDelete]
        public async Task<Response> DeleteRole(Guid id)
        {
            return await _roleRepository.DeleteRole(id);
        }
    }
}

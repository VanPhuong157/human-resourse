using BusinessObjects.DTO.UserGroup;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.UserGroups;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private IUserGroupRepository _userGroupRepository;
        public UserGroupsController(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }
        [HttpGet]
        public async Task<PaginatedList<UserGroupDTO>> GetUserGroups(
        int pageIndex = 1,
        int pageSize = 10,
        string? name = null,
        string? role = null,
        string? user = null)
        {
            return await _userGroupRepository.GetUserGroups(pageIndex, pageSize, name, role, user);
        }

        [HttpPut("{userGroupId}")]
        public async Task<Response> EditUserGroup(Guid userGroupId, UserGroupEditDTO editDto)
        {
            return await _userGroupRepository.EditUserGroup(userGroupId, editDto);
        }

        [HttpDelete("{userGroupId}")]
        public async Task<Response> DeleteUserGroup(Guid userGroupId)
        {
            return await _userGroupRepository.DeleteUserGroup(userGroupId);
        }
        [HttpPost]
        public async Task<Response> CreateUserGroup(UserGroupCreateDTO createDto)
        {
            return await _userGroupRepository.CreateUserGroup(createDto);
        }

        [HttpGet("{userGroupId}")]
        public async Task<UserGroupDTO?> GetUserGroupById(Guid userGroupId)
        {
            return await _userGroupRepository.GetUserGroupById(userGroupId);
        }

    }
}

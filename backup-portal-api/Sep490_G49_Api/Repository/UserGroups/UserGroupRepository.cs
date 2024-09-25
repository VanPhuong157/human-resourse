using BusinessObjects.DTO.UserGroup;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.UserGroups;

namespace Repository.UserGroups
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private UserGroupDAO _dao;
        public UserGroupRepository(UserGroupDAO dao)
        {
            _dao = dao;
        }

        public async Task<Response> CreateUserGroup(UserGroupCreateDTO createDto)
        {
            return await _dao.CreateUserGroup(createDto);
        }

        public async Task<Response> DeleteUserGroup(Guid id)
        {
            return await _dao.DeleteUserGroup(id);
        }

        public async Task<Response> EditUserGroup(Guid id, UserGroupEditDTO editDto)
        {
            return await _dao.EditUserGroup(id, editDto);
        }

        public async Task<UserGroupDTO?> GetUserGroupById(Guid id)
        {
            return await _dao.GetUserGroupById(id);
        }

        public async Task<PaginatedList<UserGroupDTO>> GetUserGroups(int pageIndex = 1, int pageSize = 10, string? name = null, string? role = null, string? user = null)
        {
            return await _dao.GetUserGroups(pageIndex, pageSize, name, role, user);
        }
    }
}

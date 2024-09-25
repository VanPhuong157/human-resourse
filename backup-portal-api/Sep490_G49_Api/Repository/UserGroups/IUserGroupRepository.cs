using BusinessObjects.DTO.UserGroup;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.UserGroups
{
    public interface IUserGroupRepository
    {
        Task<PaginatedList<UserGroupDTO>> GetUserGroups(
            int pageIndex = 1,
            int pageSize = 10,
            string? name = null,
            string? role = null,
            string? user = null);
        Task<Response> EditUserGroup(Guid id, UserGroupEditDTO editDto);
        Task<Response> DeleteUserGroup(Guid id);
        Task<Response> CreateUserGroup(UserGroupCreateDTO createDto);
        Task<UserGroupDTO?> GetUserGroupById(Guid id);
    }
}

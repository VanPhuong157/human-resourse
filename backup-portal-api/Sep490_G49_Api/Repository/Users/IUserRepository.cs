using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.Users
{
    public interface IUserRepository
    {
        Task<Response> Create(NewUserDTO newUser);
        Task<Response> Login(LoginDTO user);
        Task<Response> ChangePassword(Guid id, ChangePasswordDTO change);
        Task<Token> RefreshToken(string token);
        Task<Response> UpdateRoleDepartment(UpdateRoleDTO updateRoleDTO);
        Task<Response> ForgotPassword(string email);
        Task<Response> DeleteUser(Guid userId);
        Task<PaginatedList<UserGroup_UserDTO>> GetUsers(int pageIndex = 1, int pageSize = 10);
        int GetTotalUserCount(Guid? departmentId);
        double CalculateUserGrowthPercentage(Guid? departmentId);
    }
}

using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Users;

namespace Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO user;

        public UserRepository(UserDAO user)
        {
            this.user = user;
        }

        public async Task<Response> Create(NewUserDTO newUser)
        {
            return await user.CreateUser(newUser);
        }

        public async Task<Response> ChangePassword(Guid id, ChangePasswordDTO change)
        {
            return await user.ChangePassword(id, change);
        }

        public async Task<Response> Login(LoginDTO login)
        {
            return await user.Login(login);
        }

        public async Task<Token> RefreshToken(string token)
        {
            return await user.RefreshToken(token);
        }

        public async Task<Response> UpdateRoleDepartment(UpdateRoleDTO updateRoleDTO)
        {
            return await user.UpdateRoleDepartment(updateRoleDTO);
        }
        public async Task<Response> ForgotPassword(string email)
        {
           return await user.ForgotPassword(email);
        }

        public async Task<Response> DeleteUser(Guid userId)
        {
            return await user.DeleteUser(userId);
        }

        public async Task<PaginatedList<UserGroup_UserDTO>> GetUsers(int pageIndex = 1, int pageSize = 10)
        {
            return await user.GetUsers(pageIndex, pageSize);
        }
    }
}

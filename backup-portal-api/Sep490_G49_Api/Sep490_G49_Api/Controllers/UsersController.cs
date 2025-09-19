using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Users;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<Response> Create([FromBody] NewUserDTO newUser)
        {
            return await _userRepository.Create(newUser);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<Response> Login([FromBody] LoginDTO loginDTO)
        {
            return await _userRepository.Login(loginDTO);
        }

        [HttpPut("change-password/{userId}")]
        //[Authorize]
        public async Task<Response> ChangePassword(Guid userId, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            return await _userRepository.ChangePassword(userId, changePasswordDTO);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<Token> RefreshToken(string token)
        {
            return await _userRepository.RefreshToken(token);
        }

        [HttpPost("change-role-department")]
        public async Task<Response> UpdateRoleDepartment(UpdateRoleDTO updateRoleDTO)
        {
            return await _userRepository.UpdateRoleDepartment(updateRoleDTO);
        }
        [HttpPut("forgot-password")]
        public async Task<Response> ForgotPassword(string email)
        {              
                return await _userRepository.ForgotPassword(email);           
        }
        [HttpGet]
        public async Task<PaginatedList<UserGroup_UserDTO>> GetUsers(int pageIndex = 1, int pageSize = 10)
        {
            return await _userRepository.GetUsers(pageIndex, pageSize);
        }

        [HttpDelete("{id}")]
        public async Task<Response> DeleteUser(Guid id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}

using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.UserInformations;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationsController : ControllerBase
    {
        private IUserInformationRepository _repository;
        public UserInformationsController(IUserInformationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserDetails([FromForm] UserDetailsDTO userDetailsDto)
        {
            if (userDetailsDto == null)
            {
                return BadRequest(new { message = "User details are required." });
            }

            var savedUserDetailsDto = await _repository.SaveUserDetails(userDetailsDto);
            return Ok(savedUserDetailsDto);
        }

        [HttpPut]
        public async Task<IActionResult> EditUserDetails([FromForm] UserDetailsDTO userDetailsDto)
        {
            if (userDetailsDto == null)
            {
                return BadRequest(new { message = "User details are required." });
            }

            var savedUserDetailsDto = await _repository.SaveUserDetails(userDetailsDto);
            return Ok(savedUserDetailsDto);
        }

        [HttpPut]
        [Route("personal/{userId}")]
        public async Task<IActionResult> EditPersonalProfile(Guid userId, [FromForm] UpdatePersonalProfileDTO userDetailsDto)
        {
            if (userDetailsDto == null)
            {
                return BadRequest(new { message = "User details are required." });
            }

            var savedUserDetailsDto = await _repository.EditPersonalProfile(userId, userDetailsDto);
            return Ok(savedUserDetailsDto);
        }

        [HttpGet]
        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUsers(
            string? name = null,
            string? department = null,
            string? role = null,
            string? typeOfWork = null,
            string? status = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _repository.GetUserInformations(name, department, role, typeOfWork, status, pageIndex, pageSize);
        }

        [HttpGet]
        [Route("personal/{userId}")]
        public async Task<PersonalDetailDTO> GetPersonalByUserId(Guid userId)
        {
            return await _repository.GetPersonalByUserId(userId);
        }

        [HttpGet]
        [Route("user-information/{userInformationId}")]
        public async Task<UserDetailsWithoutFamilyDTO> GetById(Guid userInformationId)
        {
            return await _repository.GetById(userInformationId);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<UserDetailsWithoutFamilyDTO> GetByUserId(Guid userId)
        {
            return await _repository.GetByUserId(userId);
        }

        [HttpGet]
        [Route("family/user/{userId}")]
        public async Task<UserFamilyDTO> GetUserFamily(Guid userId)
        {
            return await _repository.GetUserFamily(userId);
        }

        [HttpPost("family/user/{userId}")]
        public async Task<Response> CreateFamilyMember(Guid userId, CreateUpdateFamilyDTO familyDto)
        {

            return await _repository.CreateFamilyMember(userId, familyDto);
        }

        [HttpPut("family/user/{userId}")]
        public async Task<Response> EditFamilyMember(Guid userId, Guid memberId, CreateUpdateFamilyDTO familyDto)
        {

            return await _repository.EditFamilyMember(userId, memberId, familyDto);
        }
        [HttpPut("{userId}/status")]

        public async Task<Response> EditStatus(Guid userId, EditStatusDTO editStatusDTO)
        {
            return await _repository.EditStatus(userId, editStatusDTO);
        }
        [HttpDelete("user-family/{memberId}")]
        public async Task<Response> DeleteFamily(Guid memberId)
        {
            return await _repository.DeleteFamily(memberId);
        }

        [HttpGet("by-department-id")]
        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUsersByDepartment(
            string? name = null,
            Guid? departmentId = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _repository.GetUserInformationsByDepartment(name, departmentId, pageIndex, pageSize);
        }

        [HttpGet("download-image/{id}")]
        public async Task<IActionResult> GetAvatarImage(Guid id)
        {
            // Lấy thông tin ứng viên từ repository
            var userInformation = await _repository.GetAvatarImageByIdAsync(id);

            if (userInformation == null || string.IsNullOrEmpty(userInformation.Avatar))
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "CV not found." });
            }

            try
            {
                var fileBytes = await _repository.GetAvatarFileAsync(userInformation.Avatar);
                return File(fileBytes, "image/jpg", $"{userInformation.FullName}.png");
            }
            catch (FileNotFoundException)
            {
                return NotFound(new Response { Code = ResponseCode.NotFound, Message = "Image file not found on server." });
            }
        }
    }
}

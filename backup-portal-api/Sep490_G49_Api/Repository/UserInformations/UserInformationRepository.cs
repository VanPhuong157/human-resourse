using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.UserInformations;
using Microsoft.AspNetCore.Hosting;

namespace Repository.UserInformations
{
    public class UserInformationRepository : IUserInformationRepository
    {
        private UserInformationDAO _dao;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserInformationRepository(UserInformationDAO dao, IWebHostEnvironment webHostEnvironment)
        {
            _dao = dao;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<Response> CreateFamilyMember(Guid userId, CreateUpdateFamilyDTO familyDto)
        {
            return await _dao.CreateFamilyMember(userId, familyDto);
        }

        public async Task<UserDetailsWithoutFamilyDTO> GetById(Guid id)
        {
            return await _dao.GetById(id);
        }

        public async Task<UserDetailsWithoutFamilyDTO> GetByUserId(Guid userId)
        {
            return await _dao.GetByUserId(userId);
        }

        public async Task<UserFamilyDTO> GetUserFamily(Guid userId)
        {
            return await _dao.GetUserFamily(userId);
        }

        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformations(
            string? name = null,
            string? department = null,
            string? role = null,
            string? typeOfWork = null,
            string? status = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _dao.GetUserInformations(name, department, role, typeOfWork, status, pageIndex, pageSize);
        }

        public async Task<Response> SaveUserDetails(UserDetailsDTO userDetailsDto)
        {
            return await _dao.SaveUserDetails(userDetailsDto);
        }

        public async Task<Response> EditFamilyMember(Guid userId, Guid familyId, CreateUpdateFamilyDTO familyDto)
        {
            return await _dao.EditFamilyMember(userId, familyId, familyDto);
        }

        public async Task<Response> EditPersonalProfile(Guid userId, UpdatePersonalProfileDTO request)
        {
            return await _dao.EditPersonalProfile(userId, request);
        }

        public async Task<PersonalDetailDTO> GetPersonalByUserId(Guid userId)
        {
            return await _dao.GetPersonalByUserId(userId);
        }

        public async Task<Response> EditStatus(Guid userId, EditStatusDTO editStatusDTO)
        {
            return await _dao.EditStatus(userId, editStatusDTO);
        }
        public async Task<Response> DeleteFamily(Guid id)
        {
            return await _dao.DeleteFamily(id);
        }

        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformationsByDepartment(string? name = null, Guid? departmentId = null, int pageIndex = 1, int pageSize = 10)
        {
            return await _dao.GetUserInformationsByDepartment(name, departmentId, pageIndex, pageSize);
        }

        public async Task<UserInformation?> GetAvatarImageByIdAsync(Guid id)
        {
            return await _dao.GetAvatarImageByIdAsync(id);
        }
        public async Task<byte[]> GetAvatarFileAsync(string cvDetail)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, cvDetail.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                return await System.IO.File.ReadAllBytesAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException("Avatar Không có trên hệ thống.");
            }
        }
    }
}

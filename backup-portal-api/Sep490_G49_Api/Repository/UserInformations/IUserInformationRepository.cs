using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.UserInformations
{
    public interface IUserInformationRepository
    {
        Task<Response> SaveUserDetails(UserDetailsDTO userDetailsDto);
        Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformations(
            string? name = null,
            string? department = null,
            string? role = null,
            string? typeOfWork = null,
            string? status = null,
            int pageIndex = 1,
            int pageSize = 10);
        Task<UserDetailsWithoutFamilyDTO> GetByUserId(Guid userId);
        Task<UserDetailsWithoutFamilyDTO> GetById(Guid id);
        Task<PersonalDetailDTO> GetPersonalByUserId(Guid userId);
        Task<UserFamilyDTO> GetUserFamily(Guid userId);
        Task<Response> CreateFamilyMember(Guid userId, CreateUpdateFamilyDTO familyDto);
        Task<Response> EditFamilyMember(Guid userId, Guid familyId, CreateUpdateFamilyDTO familyDto);
        Task<Response> EditPersonalProfile(Guid userId, UpdatePersonalProfileDTO request);
        Task<Response> EditStatus(Guid userId, EditStatusDTO editStatusDTO);
        Task<Response> DeleteFamily(Guid id);
        Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformationsByDepartment(
            string? name = null,
            Guid? departmentId = null,
            int pageIndex = 1,
            int pageSize = 10);
        Task<UserInformation?> GetAvatarImageByIdAsync(Guid id);
        Task<byte[]> GetAvatarFileAsync(string cvDetail);

    }
}

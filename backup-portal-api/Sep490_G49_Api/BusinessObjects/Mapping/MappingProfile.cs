using BusinessObjects.DTO;
using BusinessObjects.DTO.Candidate;
using BusinessObjects.DTO.Department;
using BusinessObjects.DTO.HomePage;
using BusinessObjects.DTO.JobPost;
using BusinessObjects.DTO.Notification;
using BusinessObjects.DTO.Permission;
using BusinessObjects.DTO.Role;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.UserGroup;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;

namespace BusinessObjects.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile() {
            CreateMap<OKR, OKRDTO>().ReverseMap();
            CreateMap<OKR, OKRCreateDTO>().ReverseMap();
            CreateMap<OKR, OKRDetailsDTO>().ReverseMap();
            CreateMap<OkrHistory, OkrHistoryDTO>().ReverseMap();
            CreateMap<User, NewUserDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<JobPost, JobPostDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<UserInformation, UserDetailsWithoutFamilyDTO>()
                .ForMember(dest => dest.UserFiles, opt => opt.MapFrom(src => src.UserFiles));
            CreateMap<UserFile, UserFileDTO>();
            CreateMap<Candidate, CandidateDTO>().ReverseMap();
            CreateMap<Candidate, CandidateResponseDTO>()
            .ForMember(dest => dest.JobPostTitle, opt => opt.MapFrom(src => src.JobPost.Title));
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<UserInformation, UserFamilyDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyInformation));
            CreateMap<UserInformation, UserDetailsDTO>().ReverseMap();

            CreateMap<Family, FamilyDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UserHistory, UserHistoryResponseDTO>().ReverseMap();
            CreateMap<UserInformation, UpdatePersonalProfileDTO>().ReverseMap();
            CreateMap<UserInformation, PersonalDetailDTO>().ReverseMap();
            CreateMap<UserInformation, UserDetailsDTO>().ReverseMap();
            CreateMap<EditStatusDTO, UserInformation>().ReverseMap();
            CreateMap<Permission, PermissionResponseDTO>().ReverseMap();
            CreateMap<HomePage, HomePageLastestDTO>().ReverseMap();
            CreateMap<HomePage, HomePageDTO>().ReverseMap();
            CreateMap<UserGroup, UserGroupDTO>().ReverseMap();
            CreateMap<UserGroup, UserGroupDTO>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserGroup_Users.Select(ugu => ugu.User.Username)))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserGroup_Roles.Select(ugr => ugr.Role.Name)));
            CreateMap<UserGroupCreateDTO, UserGroup>();
            CreateMap<UserGroupEditDTO, UserGroup>();
            CreateMap<User, UserGroup_UserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserInformation.FullName))
            .ForMember(dest => dest.UserGroups, opt => opt.MapFrom(src => src.UserGroup_Users.Select(ugu => ugu.UserGroup.Name)));

        }
    }
}

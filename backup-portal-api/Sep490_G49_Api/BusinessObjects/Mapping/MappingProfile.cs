using BusinessObjects.DTO;
using BusinessObjects.DTO.Department;
using BusinessObjects.DTO.Notification;
using BusinessObjects.DTO.Permission;
using BusinessObjects.DTO.Policy;
using BusinessObjects.DTO.Role;
using BusinessObjects.DTO.Schedule;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.UserGroup;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.DTO.WorkFlow;
using BusinessObjects.Models;

namespace BusinessObjects.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile() {

            CreateMap<Schedule, ScheduleDTO>()
    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy")))
    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("dd/MM/yyyy")))
    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
    .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.UserInformation.FullName))
    .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.ApprovedBy != null ? src.ApprovedBy.UserInformation.FullName : null))
    .ForMember(dest => dest.ParticipantIds, opt => opt.MapFrom(src => src.Participants.Select(p => p.UserId).ToList()))
    .ForMember(dest => dest.ParticipantNames, opt => opt.MapFrom(src => src.Participants.Select(p => p.User.UserInformation.FullName).ToList()))
    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd/MM/yyyy")))
    // THÊM 2 DÒNG NÀY
    .ForMember(dest => dest.AttachmentPaths, opt => opt.MapFrom(src => src.Attachments.Select(a => $"/Uploads/{a.StoredPath}").ToList()))
    .ForMember(dest => dest.AttachmentFileNames, opt => opt.MapFrom(src => src.Attachments.Select(a => a.FileName).ToList()));

            CreateMap<CreateScheduleDTO, Schedule>();


            CreateMap<PolicyStepUser, PolicyUserTagDTO>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.User.UserInformation.FullName));

            // ==== Document (hồ sơ chính thức của Step) ====
            CreateMap<PolicyDocument, PolicyDocumentDTO>()
                .ForMember(d => d.DownloadUrl, o => o.Ignore()) // build ở Repo nếu cần (presigned/local)
                .ForMember(d => d.UploadedByName, o => o.Ignore());

            // ==== Row cho grid bên trái ====
            CreateMap<PolicyStep, PolicyStepRowDTO>()
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Note))
                // Tính số lượng tài liệu
                // Các trường fill thủ công ở Repo (để kiểm soát Join/Order)
                .ForMember(d => d.Executors, o => o.Ignore())
                .ForMember(d => d.Reviewers, o => o.Ignore())
                .ForMember(d => d.Approvers, o => o.Ignore())
                .ForMember(d => d.LatestSubmission, o => o.Ignore());

            CreateMap<PolicyStep, PolicyStepDTO>();
            CreateMap<SubmissionComment, PolicyCommentDTO>();
            CreateMap<SubmissionFile, PolicyFileDTO>();
            CreateMap<Submission, SubmissionSummaryDTO>(); // (nếu muốn map cơ bản)

            CreateMap<OKR, OKRDTO>().ReverseMap();
            CreateMap<OKR, OKRCreateDTO>().ReverseMap();
            CreateMap<OkrHistory, OkrHistoryDTO>().ReverseMap();
            CreateMap<User, NewUserDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<UserInformation, UserDetailsWithoutFamilyDTO>()
                .ForMember(dest => dest.UserFiles, opt => opt.MapFrom(src => src.UserFiles));
            CreateMap<UserFile, UserFileDTO>();
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

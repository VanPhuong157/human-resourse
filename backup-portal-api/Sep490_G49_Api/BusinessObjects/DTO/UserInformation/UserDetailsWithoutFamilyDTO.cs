using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UserDetailsWithoutFamilyDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MatitalStatus { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DateOfBirth { get; set; }
        public string? AddressOfBirth { get; set; }
        public string? Address { get; set; }
        public string? HomeTown { get; set; }
        public string? Ethnic { get; set; }
        public string? Religious { get; set; }
        public string? AcademicLevel { get; set; }
        public bool IsPartyMember { get; set; }
        public bool IsUnionMember { get; set; }
        public string? BankingNo { get; set; }
        public string? IdCardNo { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DateOfProvide { get; set; }
        public string? AddressOfProvide { get; set; }
        public string? PitCode { get; set; }
        public string? SiCode { get; set; }
        public string? HiCode { get; set; }
        public string? PassportNo { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? PassportIssuedDate { get; set; }
        public string? PassportIssuedAddress { get; set; }
        public string? DriverLicenseNo { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DriverLicenseIssueDate { get; set; }
        public string? DriverLicenseIssuePlace { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public string? TypeOfWork { get; set; }
        public string? HealthyStatus { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Avatar { get; set; }
        public List<UserFileDTO> UserFiles { get; set; } = new List<UserFileDTO>();
        public string? DepartmentName { get; set; }
        public string? RoleNames { get; set; }
    }
}

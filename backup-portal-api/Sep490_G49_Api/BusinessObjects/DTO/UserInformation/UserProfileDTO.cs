using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UserProfileDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string Gender { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime Dob { get; set; }
        public string AddressOfBirth { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Ethnic { get; set; }
        public string Religious { get; set; } = string.Empty;
        public string CCCD { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime DateOfProvide { get; set; }
        public string AddressOfProvide { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string StudyLevel { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string? UserImage { get; set; }
    }
}

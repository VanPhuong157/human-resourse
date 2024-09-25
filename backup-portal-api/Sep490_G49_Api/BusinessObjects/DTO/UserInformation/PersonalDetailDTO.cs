using BusinessObjects.Mapping;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class PersonalDetailDTO
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string IdCardNo { get; set; }
        public string PhoneNumber { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Avatar { get; set; }
    }
}

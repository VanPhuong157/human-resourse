using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UserHistoryResponseDTO
    {
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? StartTime { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? EndTime { get; set; }
        public string? RoleName { get; set; }
        public string? DepartmentName { get; set; }
        public Guid UserId { get; set; }
    }
}

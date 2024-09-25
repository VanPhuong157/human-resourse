using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UserFileDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime UploadedAt { get; set; }
    }
}

using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public class UserFile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserInformation UserInformation { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime UploadedAt { get; set; }
    }

}

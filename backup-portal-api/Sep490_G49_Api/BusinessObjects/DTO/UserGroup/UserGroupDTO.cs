using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserGroup
{
    public class UserGroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreatedAt { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime UpdatedAt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> Users { get; set; } = new List<string>();
    }
}

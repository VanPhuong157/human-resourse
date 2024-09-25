using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.User
{
    public class UserGroup_UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreatedAt { get; set; }
        public List<string> UserGroups { get; set; }
    }
}

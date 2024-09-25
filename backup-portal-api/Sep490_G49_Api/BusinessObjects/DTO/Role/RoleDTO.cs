using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.Role
{
    public class RoleDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreatedAt { get; set; }
    }
}

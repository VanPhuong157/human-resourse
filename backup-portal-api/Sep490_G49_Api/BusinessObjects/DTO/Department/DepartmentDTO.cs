using BusinessObjects.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.Department
{
    public class DepartmentDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [Newtonsoft.Json.JsonProperty(Required = Newtonsoft.Json.Required.Always,PropertyName = "name")]
        public string Name { get; set; }

        public string? Description { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}

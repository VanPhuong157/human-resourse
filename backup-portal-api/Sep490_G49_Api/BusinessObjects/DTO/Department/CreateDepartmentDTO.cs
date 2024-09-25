using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.Department
{
    public class CreateDepartmentDTO
    {
        [Required(ErrorMessage = "DepartmentName is required")]
        [MaxLength(50, ErrorMessage = "DepartmentName can't be longer than 50 characters.")]
        [Newtonsoft.Json.JsonProperty(Required = Newtonsoft.Json.Required.Always, PropertyName = "DepartmentName")]
        public string DepartmentName { get; set; }
        [MaxLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string? Description { get; set; }
    }
}

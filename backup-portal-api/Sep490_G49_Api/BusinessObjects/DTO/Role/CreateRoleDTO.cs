using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.Role
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage ="RoleName is required.")]
        [MaxLength(50, ErrorMessage = "RoleName can't be longer than 50 characters.")]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        [MaxLength(10, ErrorMessage = "Type can't be longer than 10 characters.")]
        public string Type { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.UserGroup
{
    public class UserGroupCreateDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Guid> UserIds { get; set; } = new List<Guid>();

        [Required(ErrorMessage = "At least one RoleId is required.")]
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO
{
    namespace BusinessObjects.DTO
    {
        public class UpdateRoleDTO
        {
            [Required(ErrorMessage = "Role is required")]
            public Guid RoleId { get; set; }

            [Required(ErrorMessage = "Department is required")]
            public Guid DepartmentId { get; set; }

            [Required(ErrorMessage = "User is required")]
            public Guid UserId { get; set; }
        }
    }

}

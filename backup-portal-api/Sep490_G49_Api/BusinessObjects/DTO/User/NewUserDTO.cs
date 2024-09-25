using BusinessObjects.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.User
{
    public class NewUserDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(50, ErrorMessage = "UserName can't be longer than 50 characters")]
        [MinLength(6, ErrorMessage ="UserName can't be shorter than 6 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        [MinLength(6, ErrorMessage = "FullName must be at least 6 characters long")]
        [MaxLength(100, ErrorMessage = "FullName can't be longer than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "TypeOfWork is required")]
        public string TypeOfWork { get; set; }


        [Required(ErrorMessage = "Department is required")]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Guid RoleId { get; set; }
    }
}

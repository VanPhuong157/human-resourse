using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.User
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "OldPassword is required")]
        public string OldPassword { get; set; } 

        [Required(ErrorMessage = "NewPassword is required")]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long and a maximum of {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", ErrorMessage = "The NewPassword must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("NewPassword", ErrorMessage = "The NewPassword and ConfirmPassword do not match.")]
        public string ConfirmPassword { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.User
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "New password is required")]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long and a maximum of {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", ErrorMessage = "The new password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

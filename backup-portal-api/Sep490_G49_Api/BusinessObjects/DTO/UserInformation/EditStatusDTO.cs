using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.UserInformation
{
    public class EditStatusDTO
    {
        [Required(ErrorMessage = "NewStatus is required")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The first letter must be uppercase.")]
        public string NewStatus { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.Okr
{
    public class ApproveStatusUpdateDTO
    {
        [Required(ErrorMessage = "ApproveStatus is required.")]
        public string ApproveStatus { get; set; }
        [MaxLength(800, ErrorMessage = "Reason cannot exceed 800 characters.")]
        public string? Reason { get; set; }
    }
}

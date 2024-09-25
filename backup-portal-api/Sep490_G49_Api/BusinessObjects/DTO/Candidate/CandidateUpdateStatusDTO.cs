using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.Candidate
{
    public class CandidateUpdateStatusDTO
    {
        [Required(ErrorMessage = "NewStatus is required")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The first letter must be uppercase.")]
        public string NewStatus{ get; set; } 
    }
}

using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.DTO.Candidate
{
    public class CandidateDTO
    {
        [Required(ErrorMessage = "FullName is required")]
        [StringValidation(1, 50, ErrorMessage = "The name must be between 1 and 50 characters long.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        [PhoneNumberValidation(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "CV is required")]
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "The file size must not exceed 10 MB.")]
        public IFormFile? CvFile { get; set; }
        [Required(ErrorMessage = "JobPost is required")]
        public Guid JobPostId { get; set; }
    }
}

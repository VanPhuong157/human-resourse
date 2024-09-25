using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO
{
    public class OKRCreateDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Scope is required.")]
        public string Scope { get; set; }
        
        [Required(ErrorMessage = "TargetNumber is required.")]
        public int TargetNumber { get; set; }
        
        [Required(ErrorMessage = "Achieved is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Achieved must be greater than 0")]
        public int Achieved { get; set; } 

        [Required(ErrorMessage = "UnitOfTarget is required.")]
        [MaxLength(50, ErrorMessage = "UnitOfTarget cannot exceed 50 characters.")]
        public string UnitOfTarget { get; set; }

        [Required(ErrorMessage = "Cycle is required.")]
        public string Cycle { get; set; }

        [Required(ErrorMessage = "ConfidenceLevel is required.")]
        [MaxLength(50, ErrorMessage = "Confidence Level cannot exceed 50 characters.")]
        public string ConfidenceLevel { get; set; }
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "The file size must not exceed 10 MB.")]
        public IFormFile? ActionPlan { get; set; }

        [MaxLength(2000, ErrorMessage = "Result cannot exceed 2000 characters.")]
        public string? Result { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public Guid DepartmentId { get; set; }

        public Guid? ParentId { get; set; }
    }
}

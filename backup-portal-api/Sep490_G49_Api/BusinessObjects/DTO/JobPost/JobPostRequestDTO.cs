using BusinessObjects.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.JobPost
{
    public class JobPostRequestDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [MaxLength(3000, ErrorMessage = "Description cannot be longer than 3000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "NumberOfRecruits is required")]
        [Range(0, int.MaxValue, ErrorMessage = "NumberOfRecruits must be a positive number.")]
        public int NumberOfRecruits { get; set; }

        [Required(ErrorMessage = "Requirements is required")]
        [MaxLength(3000, ErrorMessage = "Requirements cannot be longer than 3000 characters")]
        public string Requirements { get; set; }

        [Required(ErrorMessage = "Benefits is required")]
        [MaxLength(3000, ErrorMessage = "Benefits cannot be longer than 3000 characters")]
        public string Benefits { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public int Salary { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "ExperienceYear must be a positive number.")]
        public double ExperienceYear { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [MaxLength(50, ErrorMessage = "Type cannot be longer than 50 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The first letter must be uppercase.")]
        [MaxLength(20, ErrorMessage = "Status cannot be longer than 20 characters")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public Guid Department { get; set; }
    }
}

using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.JobPost
{
    public class JobPostDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedDate { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpiryDate { get; set; }
        public int NumberOfRecruits { get; set; }
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        public int Salary { get; set; }
        public double ExperienceYear { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public bool IsDelete { get; set; }
        public Guid DepartmentId { get; set; }
        public string? CreatedBy { get; set; }
        public string? DepartmentName { get; set; }
    }
}

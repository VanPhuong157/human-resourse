using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class JobPost
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedDate { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpiryDate { get; set; }
        public int NumberOfRecruits { get; set; } 
        public string? Requirements { get; set; } 
        public string? Benefits { get; set; } 
        public int Salary { get; set; } 
        public double ExperienceYear { get; set; }
        public string? Type { get; set; }
        public bool IsDelete { get; set; } = false;
        public string? Status { get; set; }
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public Guid? CreatedBy { get; set; }
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}

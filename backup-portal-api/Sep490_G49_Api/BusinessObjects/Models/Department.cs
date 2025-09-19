namespace BusinessObjects.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<OKR> OKRs { get; set; } = new List<OKR>();
        public ICollection<OkrDepartment> OkrDepartments { get; set; }
    }
}

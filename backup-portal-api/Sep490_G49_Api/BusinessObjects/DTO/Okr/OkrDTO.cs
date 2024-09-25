namespace BusinessObjects.DTO
{
    public class OKRDTO
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Scope { get; set; }
        public string? Owner { get; set; }
        public float? Progress { get; set; }
        public string? Status { get; set; }
        public string? Cycle { get; set; }
        public string? DepartmentName { get; set; }
        public string? ParentAlignment { get; set; }
        public Guid? ParentId { get; set; } 
        public Guid? OwnerId { get; set; }  
        public Guid? DepartmentId { get; set; }  

    }
}

public class OKRCreateDTO
{
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; }
    public string? Scope { get; set; }

    public int? TargetProgress { get; set; }
    public string? UnitOfTarget { get; set; }
    public int? TargetNumber { get; set; }
    public int? Achieved { get; set; }

    public string? ApproveStatus { get; set; }

    public DateTime? DateCreated { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Company { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? Note { get; set; }

    public List<Guid> DepartmentIds { get; set; } = new();
    public List<Guid>? OwnerIds { get; set; }
    public List<Guid>? ManagerIds { get; set; }

    public string? ConfidenceLevel { get; set; }
}

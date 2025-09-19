namespace BusinessObjects.DTO
{
    public class OKRDTO
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; } // Tên
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string? Scope { get; set; }
        public List<string>? OwnerNames { get; set; } // Danh sách Người thực hiện (dạng danh sách)
        public float? Progress { get; set; } // Tiến độ công việc
        public string? Status { get; set; } // Trạng thái
        public string? Cycle { get; set; }
        public List<string>? DepartmentName { get; set; } // Danh sách tên Department
        public string? ParentAlignment { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid>? OwnerId { get; set; } // ID của Người thực hiện
        public List<string>? ManagerNames { get; set; } // Danh sách Người giao
        public int? TargetProgress { get; set; }
        public int? TargetNumber { get; set; }
        public int? Achieved { get; set; }
        public string? UnitOfTarget { get; set; }
        public string? ApproveStatus { get; set; }
        public string? Reason { get; set; }
        public string? ConfidenceLevel { get; set; }
        public string? ActionPlan { get; set; }
        public string? ActionPlanDetail { get; set; }
        public string? Result { get; set; }
        public DateTime DateCreated { get; set; } // Ngày tạo
        public DateTime? DueDate { get; set; } // Ngày đến hạn
        public string? Priority { get; set; } // Ưu tiên
        public string? Company { get; set; } // Công ty
        public DateTime? LastUpdated { get; set; } // Ngày chỉnh sửa lần cuối
        public string? Note { get; set; }
    }
}
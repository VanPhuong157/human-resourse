using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.DTO
{

        public class OKRCreateDTO
        {
            public string? Title { get; set; }
            public string? Content { get; set; }
            public string? Type { get; set; }
            public string? Scope { get; set; }
            public List<Guid>? OwnerIds { get; set; } // Danh sách OwnerIds (bao gồm người tạo)
            public float? Progress { get; set; }
            public List<string>? OwnerNames { get; set; } // Có thể bỏ nếu chỉ dùng OwnerIds
            public List<string>? ManagerNames { get; set; } // Có thể bỏ nếu chỉ dùng OwnerIds
            public int? TargetProgress { get; set; }
            public int? TargetNumber { get; set; }
            public int? Achieved { get; set; }
            public string? UnitOfTarget { get; set; }
            public string? Status { get; set; }
            public string? ApproveStatus { get; set; }
            public string? Reason { get; set; }
            public string? Cycle { get; set; }
            public string? ConfidenceLevel { get; set; }
            public string? ActionPlan { get; set; }
            public string? ActionPlanDetail { get; set; }
            public string? Result { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DueDate { get; set; }
            public string? Priority { get; set; }
            public string? Company { get; set; }
            public DateTime? LastUpdated { get; set; }
            public string? Note { get; set; }
            [NotMapped]
            public IFormFile? ActionPlanFile { get; set; } // File upload
            public Guid DepartmentId { get; set; }
            public List<string>? DepartmentName { get; set; } // Có thể bỏ nếu dùng DepartmentId
            public Guid? ParentId { get; set; }
            public string? ParentAlignment { get; set; }
            public Guid? OwnerId { get; set; } // ID của Owner đầu tiên (tùy chọn)
        }
    }

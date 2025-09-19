using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BusinessObjects.Mapping;
using Microsoft.AspNetCore.Http;

namespace BusinessObjects.Models
{
    public class OKR
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string? Scope { get; set; }
        public string? DepartmentName { get; set; }
        public int Progress { get; set; }
        public int TargetProgress { get; set; }
        public int TargetNumber { get; set; }
        public int Achieved { get; set; }
        public string UnitOfTarget { get; set; }
        public string? Status { get; set; }
        public string? ApproveStatus { get; set; }
        public string? Reason { get; set; }
        public string? Cycle { get; set; }
        public string? ConfidenceLevel { get; set; }
        public string? ActionPlan { get; set; }
        public string? ActionPlanDetail { get; set; }
        [NotMapped]
        public IFormFile? PlanCv { get; set; }
        public string? Result { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<OkrHistory> OkrHistories { get; set; } = new List<OkrHistory>();

        // Thêm các field từ DailyTask
        public DateTime? DueDate { get; set; } // Due date
        public string? Priority { get; set; } // Priority (High, Medium, Low)
        public string? Company { get; set; } // Công ty (VHS, VNEB)
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastUpdated { get; set; } // Last updated
        public string? Note { get; set; } // Note từ DailyTask

        // Quan hệ nhiều-nhiều với User (Owner và Manager)
        public ICollection<OkrUser> OkrUsers { get; set; }

        // Quan hệ nhiều-nhiều với Department (nếu cần nhiều Department)
        public ICollection<OkrDepartment> OkrDepartments { get; set; }
    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [JsonConverter(typeof(DateTimeConverter))] // Nếu dùng converter
        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndDate { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
        public ICollection<ScheduleAttachment> Attachments { get; set; } = new List<ScheduleAttachment>();
        public Guid CreatorId { get; set; }
        public User Creator { get; set; } = null!;
        public ICollection<ScheduleParticipant> Participants { get; set; } = new List<ScheduleParticipant>();
        public ScheduleStatus Status { get; set; } = ScheduleStatus.Pending;
        public Guid? ApprovedById { get; set; }
        public User? ApprovedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(7);
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(7);
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public enum ScheduleStatus
    {
        Pending,
        Approved,
        Rejected
    }
}

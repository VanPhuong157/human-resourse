using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    // 4) Hồ sơ (yêu cầu/văn bản) gắn với 1 step
    public class Submission
    {
        public Guid Id { get; set; }
        public Guid PolicyStepId { get; set; }
        public PolicyStep PolicyStep { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }

        // NEW: điều phối lượt reviewer
        public Guid? CurrentReviewerId { get; set; }

        // NEW: reviewer được chỉ định cho vòng sau khi Resubmit
        public Guid? NextReviewerId { get; set; }
        public ICollection<SubmissionParticipant> Participants { get; set; } = new List<SubmissionParticipant>();
        public ICollection<SubmissionDepartment> Departments { get; set; } = new List<SubmissionDepartment>();
        public ICollection<SubmissionEvent> Events { get; set; } = new List<SubmissionEvent>();
        public ICollection<SubmissionFile> Files { get; set; } = new List<SubmissionFile>();
        public ICollection<SubmissionComment> Comments { get; set; } = new List<SubmissionComment>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    // 7) Event mốc thời gian (timeline)
    public class SubmissionEvent
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = default!;
        public string Action { get; set; } = default!;      // Submit/StartReview/RequestChanges/Pass/Approve/Reject...
        public string FromStatus { get; set; } = default!;
        public string ToStatus { get; set; } = default!;
        public Guid ByUserId { get; set; }
        public string ByRole { get; set; } = default!;      // Executor/Reviewer/Approver
        public string? Note { get; set; }
        // NEW: log người mục tiêu (pass/back chỉ định)
        public Guid? TargetUserId { get; set; }
        public DateTime At { get; set; } = DateTime.UtcNow;

    }
}

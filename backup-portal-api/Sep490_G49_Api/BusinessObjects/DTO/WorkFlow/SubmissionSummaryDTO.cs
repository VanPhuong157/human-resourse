using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.WorkFlow
{
    public class SubmissionSummaryDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? InReviewAt { get; set; }
        public DateTime? NeedsChangesAt { get; set; }
        public DateTime? ResubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public int FileCount { get; set; }
        public int CommentCount { get; set; }
    }
}

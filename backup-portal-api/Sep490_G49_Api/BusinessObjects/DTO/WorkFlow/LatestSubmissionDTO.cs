using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.WorkFlow
{
    public class LatestSubmissionDTO
    {
        public Guid? Id { get; set; }
        public string? Status { get; set; }            // Draft/Submitted/InReview/NeedsChanges/ForApproval/Approved/Rejected
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? InReviewAt { get; set; }
        public DateTime? NeedsChangesAt { get; set; }
        public DateTime? ForApprovalAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
    }
}

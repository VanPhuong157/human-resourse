using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyHistoryDTO
    {
        public Guid Id { get; set; }
        public Guid PolicyStepId { get; set; }
        public string Title { get; set; } = default!;
        public string? Content { get; set; }
        public string? Status { get; set; }    // Draft/Submitted/InReview/NeedsChanges/Resubmitted/Approved/Rejected
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}

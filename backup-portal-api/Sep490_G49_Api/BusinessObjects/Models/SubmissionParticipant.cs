using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{

    // 5) Snapshot người tham gia của submission (list + role)
    public class SubmissionParticipant
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string Role { get; set; }
        public bool IsLead { get; set; } = false;
        public int OrderIndex { get; set; } = 0;
    }
}

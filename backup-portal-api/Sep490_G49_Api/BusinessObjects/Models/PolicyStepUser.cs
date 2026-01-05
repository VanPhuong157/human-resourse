using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class PolicyStepUser
    {
        public Guid Id { get; set; }                 // dùng surrogate key như bạn hay dùng
        public Guid PolicyStepId { get; set; }
        public PolicyStep PolicyStep { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }    // dùng model User hiện có của bạn

        public string Role { get; set; }// Executor | Reviewer | Approver
        public bool IsLead { get; set; }      // ai chủ trì
        public int OrderIndex { get; set; } 
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}

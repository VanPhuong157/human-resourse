using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class UpdatePolicyStepDTO
    {
        public DateTime? ExecDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? LawRef { get; set; }
        public string? Content { get; set; }

        // Người
        public List<Guid>? ExecutorIds { get; set; }
        public List<Guid>? ReviewerIds { get; set; }
        public List<Guid>? ApproverIds { get; set; }

        // Phòng ban theo role (NEW)
        public List<Guid>? ExecutorDepartmentIds { get; set; }
        public List<Guid>? ReviewerDepartmentIds { get; set; }
        public List<Guid>? ApproverDepartmentIds { get; set; }
    }
}

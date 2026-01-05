using BusinessObjects.DTO.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyStepRowDTO
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public DateTime? ExecDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? LawRef { get; set; }
        public Guid? ParentId { get; set; }

        // Người
        public List<PolicyUserTagDTO> Executors { get; set; } = new();
        public List<PolicyUserTagDTO> Reviewers { get; set; } = new();
        public List<PolicyUserTagDTO> Approvers { get; set; } = new();

        // Phòng ban theo từng role (NEW)
        public List<PolicyStepDepartmentDTO> ExecutorDepartments { get; set; } = new();
        public List<PolicyStepDepartmentDTO> ReviewerDepartments { get; set; } = new();
        public List<PolicyStepDepartmentDTO> ApproverDepartments { get; set; } = new();

        public LatestSubmissionDTO? LatestSubmission { get; set; }
    }
}

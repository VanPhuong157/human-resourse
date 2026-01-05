using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.WorkFlow
{
    // Hàng trên bảng 1 trang
    public class StepRowDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public IEnumerable<UserLiteDTO> Executors { get; set; }
        public IEnumerable<UserLiteDTO> Reviewers { get; set; }
        public IEnumerable<UserLiteDTO> Approvers { get; set; }
        public IEnumerable<DepartmentLiteDTO> Departments { get; set; }
        public SubmissionSummaryDTO? LatestSubmission { get; set; }
    }
}

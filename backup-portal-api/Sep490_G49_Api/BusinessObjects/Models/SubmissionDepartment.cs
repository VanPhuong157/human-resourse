using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    // 6) Snapshot phòng ban của submission
    public class SubmissionDepartment
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = default!;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
    }
}

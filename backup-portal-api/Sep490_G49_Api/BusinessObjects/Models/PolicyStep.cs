using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    // 1) Bước/tiến trình cố định (tương đương OKR – bản ghi tĩnh, FE chỉ đọc)
    public class PolicyStep
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public PolicyStep? Parent { get; set; }
        public ICollection<PolicyStep> Children { get; set; } 

        public string? Code { get; set; }
        public string? Title { get; set; }
        public int OrderIndex { get; set; }
        public string? Note { get; set; }    // FE -> Content

        // NEW: FE inline edit
        public DateTime? ExecDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? LawRef { get; set; }

        // Quan hệ
        public ICollection<PolicyStepUser> PolicyStepUsers { get; set; } 
        public ICollection<PolicyStepDepartment> PolicyStepDepartments { get; set; } 

        // NEW: hồ sơ chính thức của bước (publish từ Submission)
        public ICollection<PolicyDocument> Documents { get; set; } 
    }
}

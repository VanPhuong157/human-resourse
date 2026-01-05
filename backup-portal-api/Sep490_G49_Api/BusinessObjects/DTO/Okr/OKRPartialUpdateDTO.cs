using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Okr
{
    public class OKRPartialUpdateDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string? Scope { get; set; }
        public int? TargetNumber { get; set; }
        public int? Achieved { get; set; }          // chỉ áp dụng khi được phép
        public string? UnitOfTarget { get; set; }
        public string? Cycle { get; set; }
        public string? ConfidenceLevel { get; set; }
        public string? Result { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Company { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }         // nếu bạn muốn cho sửa tay (không khuyến khích)
    }

}

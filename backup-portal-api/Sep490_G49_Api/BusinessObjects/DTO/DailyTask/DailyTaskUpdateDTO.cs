using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.DailyTask
{
    public class DailyTaskUpdateDTO
    {
        public string? Title { get; set; }
        public string? Note { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }
        public Guid? OkrId { get; set; }
        public int? Achieved { get; set; }
        public int? TargetNumber { get; set; }
    }
}

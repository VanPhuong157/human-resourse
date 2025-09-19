using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.DailyTask
{
    public class DailyTaskCreateDTO
    {
        public string? Title { get; set; }
        public Guid? OkrId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? ManagerId { get; set; } // Tham chiếu đến UserId
        public string? DepartmentName { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Company { get; set; }
    }
}

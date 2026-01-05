using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Statistic
{
    public class WorkDashboardDTO
    {
        public int TotalTasks { get; set; }

        // key: "Normal", "Important", ...
        public IDictionary<string, int> ByPriority { get; set; } =
            new Dictionary<string, int>();

        // key: "NotStarted", "Ongoing", "Stalled", "Completed"
        public IDictionary<string, int> ByStatus { get; set; } =
            new Dictionary<string, int>();

        public IList<UserTaskStatisticItemDTO> ByUser { get; set; } =
            new List<UserTaskStatisticItemDTO>();

        public IList<DateTaskStatisticDTO> ByDate { get; set; } =
            new List<DateTaskStatisticDTO>();
    }
}

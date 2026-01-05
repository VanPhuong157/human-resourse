using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Schedule
{
    public class CreateScheduleDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
        public List<IFormFile>? Files { get; set; }
        public List<Guid> ParticipantIds { get; set; } = new List<Guid>();

    }
}

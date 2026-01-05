using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ScheduleAttachment
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string StoredPath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}

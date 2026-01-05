using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class OkrHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DateCreated { get; set; }
        public int? OldProgress { get; set; }
        public int? NewProgress { get; set; }
        public int? OldAchieved { get; set; }
        public int? NewAchieved { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? UnitOfTarget { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Guid OkrId { get; set; }
        public OKR OKR { get; set; }

        public ICollection<CommentFile> Attachments { get; set; } = new List<CommentFile>();
    }
}

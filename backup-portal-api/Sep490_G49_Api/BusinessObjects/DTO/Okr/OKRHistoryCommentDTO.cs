using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Okr
{
    public class OKRHistoryCommentDTO
    {
        public string? UserName { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreatedAt { get; set; }       
        public string? Description { get; set; }
        public Guid? OkrId { get; set; }
    }
}

using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Notification
{
    public class NotificationDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public Guid UserId { get; set; }
        public string? RedirectUrl { get; set; }
    }
}

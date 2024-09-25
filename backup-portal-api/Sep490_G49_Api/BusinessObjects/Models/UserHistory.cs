using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class UserHistory
    {
        public Guid Id { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? StartTime { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? EndTime { get; set; }
        public string? RoleName { get; set; }
        public string? DepartmentName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public class DepartmentHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public string? Position { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartDate { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public virtual Department? Department { get; set; }
        public virtual UserInformation? User { get; set; }
    }
}

using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public class Family
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Relationship { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateOfBirth { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; } = null!;
    }
}

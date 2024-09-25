using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.UserInformation
{
    public class FamilyDTO
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Relationship { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime DateOfBirth { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

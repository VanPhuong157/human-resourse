using BusinessObjects.DTO.Department;
using BusinessObjects.Mapping;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.UserInformation
{
    public class ListUserDTO
    {
        public string? UserImage { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime Dob { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime StartDate { get; set; }
        public virtual ICollection<DepartmentHistoryDTO> DepartmentHistories { get; set; }
        public string CCCD { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}

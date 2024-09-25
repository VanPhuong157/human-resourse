using BusinessObjects.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace BusinessObjects.DTO.Okr
{
    public class OKRRequestDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Scope { get; set; }
        public string? Owner { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateCreated { get; set; }
        public string? ApproveStatus { get; set; }
        public string? DepartmentName { get; set; }
        public string? Cycle { get; set; }

        public string? ParentAligment { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}

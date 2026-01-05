using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyStepDTO
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public int OrderIndex { get; set; }
        public string? Note { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyStepUserDTO
    {
        public Guid Id { get; set; }   // link id
        public Guid UserId { get; set; }
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public bool IsLead { get; set; }
        public int OrderIndex { get; set; }
    }
}

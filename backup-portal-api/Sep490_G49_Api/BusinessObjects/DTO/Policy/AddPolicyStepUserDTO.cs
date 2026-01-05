using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{

    public class AddPolicyStepUserDTO
    {
        public Guid UserId { get; set; }
        public string Role { get; set; } 
        public bool IsLead { get; set; } = false;
        public int OrderIndex { get; set; } = 0;
    }
}

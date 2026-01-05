using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyStepDepartmentDTO
    {
        public Guid Id { get; set; }           // link id
        public Guid DepartmentId { get; set; }
        public string Name { get; set; } = default!;
    }
}

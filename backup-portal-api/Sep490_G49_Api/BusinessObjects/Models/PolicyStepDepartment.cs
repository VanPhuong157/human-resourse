using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class PolicyStepDepartment
    {
        public Guid Id { get; set; }
        public Guid PolicyStepId { get; set; }
        public PolicyStep PolicyStep { get; set; } 

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public string Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class OkrDepartment
    {
        public Guid OkrId { get; set; }
        public OKR Okr { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

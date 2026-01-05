using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.WorkFlow
{
    public class PassOrBackRequest
    {
        public string? Note { get; set; }
        public Guid? ToUserId { get; set; }
    }
}

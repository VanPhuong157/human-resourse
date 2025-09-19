using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class OkrUser
    {
        public Guid OkrId { get; set; }
        public OKR Okr { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

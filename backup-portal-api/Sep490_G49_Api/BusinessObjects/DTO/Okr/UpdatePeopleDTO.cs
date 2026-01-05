using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Okr
{
    public class UpdatePeopleDTO
    {
        public List<Guid>? OwnerIds { get; set; }
        public List<Guid>? ManagerIds { get; set; }
    }
}

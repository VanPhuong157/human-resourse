using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.WorkFlow
{
    public class UpdateSubmissionFileDTO
    {
        public SubmissionFileCategory? Category { get; set; }
        public bool? IsSelectedForPublish { get; set; }
    }
}

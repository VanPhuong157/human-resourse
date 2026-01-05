using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyCommentDTO
    {
        public Guid Id { get; set; }
        public Guid ByUserId { get; set; }
        public string ByRole { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

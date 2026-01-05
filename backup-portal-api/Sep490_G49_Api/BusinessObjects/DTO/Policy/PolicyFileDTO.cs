using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyFileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = default!;
        public string StoredPath { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public long FileSize { get; set; }
        public bool IsImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

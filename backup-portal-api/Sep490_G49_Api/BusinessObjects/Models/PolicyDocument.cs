using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public enum DocumentCategory { General = 0, Approval = 1 }

    public class PolicyDocument
    {
        public Guid Id { get; set; }

        public Guid PolicyStepId { get; set; }
        public PolicyStep Step { get; set; } = default!;

        public string FileName { get; set; } = default!;
        public long FileSize { get; set; }                 // bytes
        public string StoragePath { get; set; } = default!;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Guid UploadedBy { get; set; }

        public DocumentCategory Category { get; set; } = DocumentCategory.General;

    }
}

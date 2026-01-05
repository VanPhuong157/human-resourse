using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public enum SubmissionFileCategory { General = 0, Approval = 1 }
    public class SubmissionFile
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = default!;

        public string FileName { get; set; } = default!;
        public string StoragePath { get; set; } = default!;
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public bool IsImage { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Guid UploadedBy { get; set; }

        // NEW
        public SubmissionFileCategory Category { get; set; } = SubmissionFileCategory.General;
        public bool IsSelectedForPublish { get; set; } = false;

        // NEW: back-link sau khi publish
        public Guid? PublishedDocumentId { get; set; }
        public PolicyDocument? PublishedDocument { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    // 9) Bình luận
    public class SubmissionComment
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = default!;
        public Guid? ByUserId { get; set; }
        public string ByRole { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? ByUser { get; set; }
    }
}

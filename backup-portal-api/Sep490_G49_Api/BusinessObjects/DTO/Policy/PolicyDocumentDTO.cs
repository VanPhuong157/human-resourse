using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Policy
{
    public class PolicyDocumentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = default!;
        public long FileSize { get; set; }
        public string DownloadUrl { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
        public string? UploadedByName { get; set; }
        public DocumentCategory Category { get; set; }
    }
}

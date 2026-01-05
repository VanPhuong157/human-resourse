using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Okr
{
    public class CommentFileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string StoredPath { get; set; }   // FE dùng trực tiếp để <img src> / download
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public bool IsImage { get; set; }
    }
}

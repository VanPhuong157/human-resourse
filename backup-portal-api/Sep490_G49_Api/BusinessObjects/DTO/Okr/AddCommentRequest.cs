using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Okr
{
    public class AddCommentRequest
    {
        [FromForm(Name = "text")]
        public string? Text { get; set; }
        [FromForm(Name = "files")]
        public IFormFileCollection? Attachments { get; set; } // optional
    }

}

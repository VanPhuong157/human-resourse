using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class HomePage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TitleBody { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string StatusJobPost { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; }

        [NotMapped]
        public IFormFile? ImageBackground { get; set; }

        public string? ImageBackgroundPath { get; set; }
        public string? ImageBackgroundDetail { get; set; }
    }
}

using BusinessObjects.Mapping;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateApply { get; set; }
        public string? Cv { get; set; }
        public string? CvDetail { get; set; }
        [NotMapped]
        public IFormFile? CvFile { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid JobPostId { get; set; }
        public JobPost JobPost { get; set; }
    }
}

using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.Candidate
{
    public class CandidateResponseDTO
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateApply { get; set; }
        public string? Cv { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid? JobPostId { get; set; }
        public string? JobPostTitle { get; set; }
    }
}

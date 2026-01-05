using BusinessObjects.DTO.Okr;
using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO
{
    public class OkrHistoryDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        public int? OldProgress { get; set; }
        public int? NewProgress { get; set; }
        public int? OldAchieved { get; set; }
        public int? NewAchieved { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? UnitOfTarget { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Guid? OkrId { get; set; }
        public List<CommentFileDTO> Attachments { get; set; } = new();
    }
}

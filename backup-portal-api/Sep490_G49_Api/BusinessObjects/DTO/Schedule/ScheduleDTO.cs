using System;
using System.Collections.Generic;

namespace BusinessObjects.DTO.Schedule
{
    public class ScheduleDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string StartDate { get; set; } = string.Empty; // Đổi sang string để format dd/MM/yyyy
        public string EndDate { get; set; } = string.Empty;
        public int Priority { get; set; } // int thay vì enum để dễ map
        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public List<Guid> ParticipantIds { get; set; } = new List<Guid>();
        public List<string> ParticipantNames { get; set; } = new List<string>();
        public int Status { get; set; }
        public Guid? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;

        // THÊM 2 DÒNG NÀY
        public List<string> AttachmentPaths { get; set; } = new List<string>(); // "/Uploads/randomname.pdf"
        public List<string> AttachmentFileNames { get; set; } = new List<string>(); // "BaoCao.pdf", "Slide.pptx"
    }
}
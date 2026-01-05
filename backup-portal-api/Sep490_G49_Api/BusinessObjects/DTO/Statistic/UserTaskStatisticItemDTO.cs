// BusinessObjects/DTO/Statistic/UserWorkStatisticDTO.cs
namespace BusinessObjects.DTO.Statistic
{
    public class UserTaskStatisticItemDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public double OnTimeRate { get; set; }     // 0..1
        public double FinalScore { get; set; }     // 0..1
    }

}

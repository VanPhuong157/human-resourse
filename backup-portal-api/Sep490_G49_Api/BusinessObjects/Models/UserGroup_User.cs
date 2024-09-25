namespace BusinessObjects.Models
{
    public class UserGroup_User
    {
        public Guid UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

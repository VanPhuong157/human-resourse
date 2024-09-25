namespace BusinessObjects.Models
{
    public class UserGroup_Role
    {
        public Guid UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

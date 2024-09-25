namespace BusinessObjects.Models
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserGroup_User> UserGroup_Users { get; set; } = new List<UserGroup_User>();
        public ICollection<UserGroup_Role> UserGroup_Roles { get; set; } = new List<UserGroup_Role>();
    }
}

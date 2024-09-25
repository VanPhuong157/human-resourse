namespace BusinessObjects.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<UserGroup_Role> UserGroup_Roles { get; set; } = new List<UserGroup_Role>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}

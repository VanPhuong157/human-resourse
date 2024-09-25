using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? AccessToken { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime AccessTokenCreated { get; set; }
        public string? RefreshToken { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime RefreshTokenCreated { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime RefreshTokenExpires { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public UserInformation? UserInformation { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserHistory> UserHistories { get; set; } = new List<UserHistory>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }

        public byte[]? TemporaryPasswordHash { get; set; }
        public byte[]? TemporaryPasswordSalt { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TemporaryPasswordExpires { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
        public ICollection<UserGroup_User> UserGroup_Users { get; set; } = new List<UserGroup_User>();  
    }
}

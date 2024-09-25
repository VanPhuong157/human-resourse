using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class UserPermission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public bool IsEnabled { get; set; }
        public User User { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}

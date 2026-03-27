using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}

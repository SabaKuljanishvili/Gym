using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        // Many-to-many with User
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

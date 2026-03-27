using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime RegistrationDate { get; set; }

        public int PersonId { get; set; }

        // Navigation properties
        public Person? Person { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        // Many-to-many with Role
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

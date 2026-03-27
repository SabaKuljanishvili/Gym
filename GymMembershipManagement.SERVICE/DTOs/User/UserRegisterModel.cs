using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagement.SERVICE.DTOs.User
{
    public class UserRegisterModel
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;

        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        [MaxLength(50)]
        public string Address { get; set; } = null!;
    }
}

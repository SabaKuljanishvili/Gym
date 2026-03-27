using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagement.SERVICE.DTOs.User
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

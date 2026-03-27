namespace GymMembershipManagement.SERVICE.DTOs.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagement.SERVICE.DTOs.Membership
{
    public class RegisterMembershipDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}

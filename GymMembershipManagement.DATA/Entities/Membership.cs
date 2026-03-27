using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Memberships")]
    public class Membership
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MembershipTypeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive =>
            DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        // Membership => User
        public User? User { get; set; }

        // Membership => MembershipType
        public MembershipType MembershipType { get; set; } = null!;

    }
}

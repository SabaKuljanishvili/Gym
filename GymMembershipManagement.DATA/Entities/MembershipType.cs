using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("MembershipTypes")]
    public class MembershipType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MembershipTypeName { get; set; } = null!;

        //MembershipType => Memberships 
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}

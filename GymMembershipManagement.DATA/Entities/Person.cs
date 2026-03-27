using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
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

        // Person => User 
        public User? User { get; set; }
    }
}

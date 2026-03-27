using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("GymClasses")]
    public class GymClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string GymClassName { get; set; } = null!;


        // GymClass => Schedules
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}

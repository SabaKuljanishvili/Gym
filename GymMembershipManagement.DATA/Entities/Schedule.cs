using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime ScheduledDateTime { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int GymClassId { get; set; }

        //  Schedule => User 
        public User? User { get; set; } 

        // Schedule => GymClass
        public GymClass? GymClass { get; set; }

        // Schedule => Reservations 
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}

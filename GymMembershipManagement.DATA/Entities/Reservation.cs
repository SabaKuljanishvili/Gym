using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Entities
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ScheduleId { get; set; }

        // Reservation => User
        public User? User { get; set; }

        // Reservation => Schedule
        public Schedule? Schedule { get; set; }
  
    }
}

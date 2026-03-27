using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagement.SERVICE.DTOs.Schedule
{
    public class AssignScheduleDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        [Required]
        [Range(1, 480, ErrorMessage = "Duration must be between 1 and 480 minutes.")]
        public int Duration { get; set; }

        [Required]
        public int GymClassId { get; set; }
    }
}

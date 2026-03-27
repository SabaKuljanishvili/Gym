namespace GymMembershipManagement.SERVICE.DTOs.Schedule
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public int Duration { get; set; }
        public int GymClassId { get; set; }
        public string? GymClassName { get; set; }
    }
}

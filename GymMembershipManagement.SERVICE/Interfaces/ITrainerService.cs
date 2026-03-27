using GymMembershipManagement.SERVICE.DTOs.Schedule;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface ITrainerService
    {
        Task<bool> AssignSchedule(AssignScheduleDTO dto);
        Task<IEnumerable<ScheduleDTO>> GetSchedulesByTrainer(int trainerId);
    }
}

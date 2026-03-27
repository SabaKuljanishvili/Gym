using GymMembershipManagement.DATA;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.DATA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipManagement.DAL.Repositories
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetByTrainerIdAsync(int trainerId);
        Task<IEnumerable<Schedule>> GetAllWithDetailsAsync();
    }

    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private readonly GymDbContext _context;

        public ScheduleRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.Schedules
                .Include(s => s.GymClass)
                .Include(s => s.User).ThenInclude(u => u!.Person)
                .Where(s => s.UserId == trainerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllWithDetailsAsync()
        {
            return await _context.Schedules
                .Include(s => s.GymClass)
                .Include(s => s.User).ThenInclude(u => u!.Person)
                .ToListAsync();
        }
    }
}

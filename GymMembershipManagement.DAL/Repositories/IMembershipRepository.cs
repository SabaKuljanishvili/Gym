using GymMembershipManagement.DATA;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.DATA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipManagement.DAL.Repositories
{
    public interface IMembershipRepository : IBaseRepository<Membership>
    {
        Task<IEnumerable<Membership>> GetAllWithDetailsAsync();
        Task<Membership?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Membership>> GetByUserIdAsync(int userId);
    }

    public class MembershipRepository : BaseRepository<Membership>, IMembershipRepository
    {
        private readonly GymDbContext _context;

        public MembershipRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Membership>> GetAllWithDetailsAsync()
        {
            return await _context.Memberships
                .Include(m => m.MembershipType)
                .Include(m => m.User).ThenInclude(u => u!.Person)
                .ToListAsync();
        }

        public async Task<Membership?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Memberships
                .Include(m => m.MembershipType)
                .Include(m => m.User).ThenInclude(u => u!.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Membership>> GetByUserIdAsync(int userId)
        {
            return await _context.Memberships
                .Include(m => m.MembershipType)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }
    }
}

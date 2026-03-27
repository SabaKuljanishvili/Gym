using GymMembershipManagement.DATA;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.DATA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipManagement.DAL.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithPersonAsync(int id);
        Task<IEnumerable<User>> GetAllWithPersonAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly GymDbContext _context;

        public UserRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithPersonAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<IEnumerable<User>> GetAllWithPersonAsync()
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == roleName))
                .ToListAsync();
        }
    }
}

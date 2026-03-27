using GymMembershipManagement.DATA;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.DATA.Infrastructure;

namespace GymMembershipManagement.DAL.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
    }
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly GymDbContext _context;
        public PersonRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using GymMembershipManagement.DATA;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.DATA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DAL.Repositories
{
    public interface IGymClassRepository : IBaseRepository<GymClass>
    {
    }

    public class GymClassRepository : BaseRepository<GymClass>, IGymClassRepository
    {
        private readonly GymDbContext _context;
        public GymClassRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

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
    public interface IRoleRepository : IBaseRepository<Role>
    {
    }
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly GymDbContext _context;
        public RoleRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

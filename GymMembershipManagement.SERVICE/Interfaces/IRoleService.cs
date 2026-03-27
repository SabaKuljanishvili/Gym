using GymMembershipManagement.SERVICE.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto model);
        Task<bool> UpdateRoleAsync(int id, UpdateRoleDto model);
        Task<bool> DeleteRoleAsync(int id);
    }
}

using GymMembershipManagement.SERVICE.DTOs.GymClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface IGymClassService
    {

        Task<IEnumerable<GymClassDto>> GetAllGymClassesAsync();
        Task<GymClassDto> GetGymClassByIdAsync(int id);
        Task<GymClassDto> CreateGymClassAsync(CreateGymClassDto model);
        Task<bool> DeleteGymClassAsync(int id);
        Task<bool> UpdateGymClassAsync(int id, UpdateGymClassDto model);

    }
}

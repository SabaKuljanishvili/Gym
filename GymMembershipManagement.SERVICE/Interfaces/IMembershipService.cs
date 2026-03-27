using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.Membership;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface IMembershipService
    {
        Task<MembershipDTO> RegisterMembership(RegisterMembershipDTO dto);
        Task<bool> RenewMembership(int membershipId);
        Task<MembershipStatusDTO> GetMembershipStatus(int customerId);
        Task<IEnumerable<MembershipDTO>> GetMembershipsByUser(int userId);
        Task<IEnumerable<MembershipDTO>> GetMembershipsByStatus();
    }
}

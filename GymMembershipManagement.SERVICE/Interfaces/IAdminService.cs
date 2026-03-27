using GymMembershipManagement.SERVICE.DTOs.User;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface IAdminService
    {
        Task<UserDTO> AddUser(UserRegisterModel model);
        Task<UserDTO> GetUserById(int userId);
        Task UpdateUserDetails(int userId, UpdateUserModel model);
        Task RemoveUser(int userId);
        Task<IEnumerable<UserDTO>> GetAllMembers();
        Task<IEnumerable<UserDTO>> GetAllTrainers();
    }
}

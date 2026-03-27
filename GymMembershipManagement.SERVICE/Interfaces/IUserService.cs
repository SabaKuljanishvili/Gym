using GymMembershipManagement.SERVICE.DTOs.User;

namespace GymMembershipManagement.SERVICE.Interfaces
{
    public interface IUserService
    {
        Task UserRegistration(UserRegisterModel model);
        Task<UserDTO> Login(string email, string password);
        Task<UserDTO> GetProfile(int userId);
        Task<UserDTO> UpdateProfile(int userId, UpdateUserModel model);
        Task DeleteProfile(int userId);
        void Logout();
    }
}

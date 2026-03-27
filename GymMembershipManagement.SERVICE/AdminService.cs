using GymMembershipManagement.DAL.Repositories;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.User;
using GymMembershipManagement.SERVICE.Interfaces;

namespace GymMembershipManagement.SERVICE
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;

        public AdminService(IUserRepository userRepository, IPersonRepository personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public async Task<UserDTO> AddUser(UserRegisterModel model)
        {
            var person = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Address = model.Address
            };
            await _personRepository.AddAsync(person);

            var user = new User
            {
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Email = model.Email,
                RegistrationDate = DateTime.UtcNow,
                PersonId = person.PersonId
            };
            await _userRepository.AddAsync(user);

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdWithPersonAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with ID {userId} not found");

            return MapToDTO(user);
        }

        public async Task UpdateUserDetails(int userId, UpdateUserModel model)
        {
            var user = await _userRepository.GetByIdWithPersonAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with ID {userId} not found");

            if (!string.IsNullOrWhiteSpace(model.Username)) user.Username = model.Username;
            if (!string.IsNullOrWhiteSpace(model.Email)) user.Email = model.Email;

            if (user.Person != null)
            {
                if (!string.IsNullOrWhiteSpace(model.FirstName)) user.Person.FirstName = model.FirstName;
                if (!string.IsNullOrWhiteSpace(model.LastName)) user.Person.LastName = model.LastName;
                if (!string.IsNullOrWhiteSpace(model.Phone)) user.Person.Phone = model.Phone;
                if (!string.IsNullOrWhiteSpace(model.Address)) user.Person.Address = model.Address;
                await _personRepository.UpdateAsync(user.Person);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new KeyNotFoundException($"User with ID {userId} not found");
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<IEnumerable<UserDTO>> GetAllMembers()
        {
            // Returns users with "Customer" role
            var users = await _userRepository.GetUsersByRoleAsync("Customer");
            return users.Select(MapToDTO);
        }

        public async Task<IEnumerable<UserDTO>> GetAllTrainers()
        {
            // Returns users with "Trainer" role
            var users = await _userRepository.GetUsersByRoleAsync("Trainer");
            return users.Select(MapToDTO);
        }

        private static UserDTO MapToDTO(User user) => new UserDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
            FirstName = user.Person?.FirstName,
            LastName = user.Person?.LastName
        };
    }
}

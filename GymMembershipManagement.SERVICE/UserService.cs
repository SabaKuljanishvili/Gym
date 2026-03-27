using GymMembershipManagement.DAL.Repositories;
using GymMembershipManagement.DATA.Entities;
using GymMembershipManagement.SERVICE.DTOs.User;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.Extensions.Logging;

namespace GymMembershipManagement.SERVICE
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IPersonRepository personRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task UserRegistration(UserRegisterModel model)
        {
            // Check if email already exists
            var existing = await _userRepository.GetByEmailAsync(model.Email);
            if (existing != null)
                throw new InvalidOperationException($"A user with email '{model.Email}' already exists.");

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
            _logger.LogInformation("User registered successfully: {Email}", model.Email);
        }

        public async Task<UserDTO> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Email and password are required.");

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                _logger.LogWarning("Login failed for email: {Email}", email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                _logger.LogWarning("Invalid password for email: {Email}", email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            _logger.LogInformation("Login successful for: {Email}", email);
            return MapToDTO(user);
        }

        public async Task<UserDTO> GetProfile(int userId)
        {
            var user = await _userRepository.GetByIdWithPersonAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            return MapToDTO(user);
        }

        public async Task<UserDTO> UpdateProfile(int userId, UpdateUserModel model)
        {
            var user = await _userRepository.GetByIdWithPersonAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            bool userChanged = false;
            bool personChanged = false;

            if (!string.IsNullOrWhiteSpace(model.Username) && model.Username != user.Username)
            {
                user.Username = model.Username;
                userChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(model.Email) && model.Email != user.Email)
            {
                user.Email = model.Email;
                userChanged = true;
            }

            if (user.Person != null)
            {
                if (!string.IsNullOrWhiteSpace(model.FirstName) && model.FirstName != user.Person.FirstName)
                { user.Person.FirstName = model.FirstName; personChanged = true; }

                if (!string.IsNullOrWhiteSpace(model.LastName) && model.LastName != user.Person.LastName)
                { user.Person.LastName = model.LastName; personChanged = true; }

                if (!string.IsNullOrWhiteSpace(model.Phone) && model.Phone != user.Person.Phone)
                { user.Person.Phone = model.Phone; personChanged = true; }

                if (!string.IsNullOrWhiteSpace(model.Address) && model.Address != user.Person.Address)
                { user.Person.Address = model.Address; personChanged = true; }

                if (personChanged) await _personRepository.UpdateAsync(user.Person);
            }

            if (userChanged) await _userRepository.UpdateAsync(user);

            return MapToDTO(user);
        }

        public async Task DeleteProfile(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            await _userRepository.DeleteAsync(userId);
        }

        public void Logout()
        {
            // Client-side token invalidation (JWT stateless — token is removed on client)
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

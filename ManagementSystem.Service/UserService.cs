using ManagementSystem.Data.Repositories;
using ManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Identity; 
using System.Threading.Tasks;



namespace ManagementSystem.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(UserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> AuthenticateAsync(string firstname, string lastname, string password)
        {
            var user = await _userRepository.GetUserByFullNameAsync(firstname, lastname);
            if (user == null)
                return null;

            // בדיקת סיסמה אם היא מוצפנת
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password); // שימוש בהזרקה במקום new
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}

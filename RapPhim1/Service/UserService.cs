using RapPhim1.DAO;
using RapPhim1.DTO.Auth;
using RapPhim1.DTO.Common;
using RapPhim1.Models;
using RapPhim1.Services;
using System.Text;
using System.Security.Cryptography;

namespace RapPhim1.Service
{
    public class UserService : IUserService
    {
        private readonly UserDAO _userDAO;
        private readonly JwtService _jwtService;

        public UserService(UserDAO userDAO, JwtService jwtService)
        {
            _userDAO = userDAO;
            _jwtService = jwtService;
        }

        public async Task<string?> Register(UserRegisterDto dto)
        {
            if (await _userDAO.GetByEmailAsync(dto.Email) != null)
                return "Email already exists";

            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = Hash(dto.Password),
                Role = "User"
            };

            await _userDAO.AddAsync(user);
            await _userDAO.SaveChangesAsync();
            return null;
        }

        public async Task<string?> Login(UserLoginDto dto)
        {
            var user = await _userDAO.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash != Hash(dto.Password))
                return null;

            return _jwtService.GenerateToken(user);
        }

        public async Task<object?> GetProfile(int userId)
        {
            var user = await _userDAO.GetByIdAsync(userId);
            if (user == null) return null;

            return new
            {
                user.Id,
                user.Email,
                user.Name,
                user.PhoneNumber,
                user.Role
            };
        }

        public async Task<string?> UpdateProfile(int userId, UserUpdateProfileDto dto)
        {
            var user = await _userDAO.GetByIdAsync(userId);
            if (user == null) return "User not found";

            user.Name = dto.Name;
            user.PhoneNumber = dto.PhoneNumber;

            await _userDAO.SaveChangesAsync();
            return null;
        }

        public async Task<string?> ChangePassword(int userId, ChangePasswordDto dto)
        {
            var user = await _userDAO.GetByIdAsync(userId);
            if (user == null) return "User not found";
            if (user.PasswordHash != Hash(dto.CurrentPassword))
                return "Current password is incorrect";

            user.PasswordHash = Hash(dto.NewPassword);
            await _userDAO.SaveChangesAsync();
            return null;
        }

        private string Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

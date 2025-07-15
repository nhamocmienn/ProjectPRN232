using RapPhim1.DTO.Auth;
using RapPhim1.DTO.Common;

namespace RapPhim1.Service
{
    public interface IUserService
    {
        Task<string?> Register(UserRegisterDto dto);
        Task<string?> Login(UserLoginDto dto);
        Task<object?> GetProfile(int userId);
        Task<string?> UpdateProfile(int userId, UserUpdateProfileDto dto);
        Task<string?> ChangePassword(int userId, ChangePasswordDto dto);
    }
}

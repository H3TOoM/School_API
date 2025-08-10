using School_API.DTOs;
using School_API.Models;

namespace School_API.Services.Base
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDto dto);
        Task<User> Login(LoginDto dto);
    }
}

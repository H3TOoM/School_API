using School_API.Models;

namespace School_API.Services.Base
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);   
    }
}

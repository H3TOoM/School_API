using school_api.Data.Models;
using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(UserReadDto user);
    }
}

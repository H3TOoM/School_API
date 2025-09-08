using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(int id);

        Task<UserReadDto> CreateUserAsync( UserCreateDto dto );

        Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto dto );

        Task<bool> DeleteUserAsync(int id);
    }
}

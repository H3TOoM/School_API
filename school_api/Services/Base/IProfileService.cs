using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IProfileService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UpdateProfileDto dto);
        Task<UserStatsDto?> GetUserStatsAsync();
    }
}

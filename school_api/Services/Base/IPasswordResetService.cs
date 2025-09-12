using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IPasswordResetService
    {
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task<bool> ValidateResetTokenAsync(string token, string email);
    }
}

using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto emailDto);
        Task<bool> SendNotificationAsync(NotificationEmailDto notificationDto);
        Task<bool> SendPasswordResetEmailAsync(string email, string resetToken);
        Task<bool> SendWelcomeEmailAsync(string email, string name);
    }
}

using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMainRepoistory<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IMainRepoistory<User> userRepository,
            IConfiguration configuration,
            ILogger<EmailService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(EmailDto emailDto)
        {
            try
            {
                // In a real application, you would integrate with an email service like:
                // - SendGrid
                // - Mailgun
                // - Amazon SES
                // - SMTP server

                _logger.LogInformation($"Sending email to {emailDto.To} with subject: {emailDto.Subject}");
                
                // Simulate email sending
                await Task.Delay(100);
                
                _logger.LogInformation($"Email sent successfully to {emailDto.To}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {emailDto.To}");
                return false;
            }
        }

        public async Task<bool> SendNotificationAsync(NotificationEmailDto notificationDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(notificationDto.UserId);
                if (user == null)
                    return false;

                var emailDto = new EmailDto
                {
                    To = user.Email,
                    Subject = GetNotificationSubject(notificationDto.Type),
                    Body = GetNotificationBody(notificationDto.Type, notificationDto.Message, notificationDto.Data),
                    IsHtml = true
                };

                return await SendEmailAsync(emailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send notification to user {notificationDto.UserId}");
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string resetToken)
        {
            try
            {
                var resetUrl = $"{_configuration["AppSettings:BaseUrl"]}/reset-password?token={resetToken}&email={email}";
                
                var emailDto = new EmailDto
                {
                    To = email,
                    Subject = "Password Reset Request",
                    Body = $@"
                        <h2>Password Reset Request</h2>
                        <p>You have requested to reset your password. Click the link below to reset your password:</p>
                        <p><a href='{resetUrl}'>Reset Password</a></p>
                        <p>This link will expire in 24 hours.</p>
                        <p>If you did not request this password reset, please ignore this email.</p>
                    ",
                    IsHtml = true
                };

                return await SendEmailAsync(emailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send password reset email to {email}");
                return false;
            }
        }

        public async Task<bool> SendWelcomeEmailAsync(string email, string name)
        {
            try
            {
                var emailDto = new EmailDto
                {
                    To = email,
                    Subject = "Welcome to School Management System",
                    Body = $@"
                        <h2>Welcome {name}!</h2>
                        <p>Welcome to the School Management System. Your account has been created successfully.</p>
                        <p>You can now log in using your credentials.</p>
                        <p>If you have any questions, please contact the administrator.</p>
                        <p>Best regards,<br>School Management Team</p>
                    ",
                    IsHtml = true
                };

                return await SendEmailAsync(emailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send welcome email to {email}");
                return false;
            }
        }

        private string GetNotificationSubject(string type)
        {
            return type switch
            {
                "attendance" => "Attendance Update",
                "grade" => "Grade Update",
                "schedule" => "Schedule Change",
                "announcement" => "New Announcement",
                _ => "Notification"
            };
        }

        private string GetNotificationBody(string type, string message, Dictionary<string, object>? data)
        {
            var baseBody = $@"
                <h2>School Management System Notification</h2>
                <p>{message}</p>
            ";

            if (data != null && data.Any())
            {
                baseBody += "<h3>Details:</h3><ul>";
                foreach (var item in data)
                {
                    baseBody += $"<li><strong>{item.Key}:</strong> {item.Value}</li>";
                }
                baseBody += "</ul>";
            }

            baseBody += "<p>Best regards,<br>School Management Team</p>";
            return baseBody;
        }
    }
}

using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;
using System.Security.Cryptography;
using System.Text;

namespace school_api.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IMainRepoistory<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PasswordResetService(
            IMainRepoistory<User> userRepository,
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetAllAsync();
            var targetUser = user.FirstOrDefault(u => u.Email == dto.Email);
            
            if (targetUser == null)
                return false;

            // Generate reset token
            var resetToken = GenerateResetToken();
            
            // In a real application, you would:
            // 1. Store the reset token in database with expiration
            // 2. Send email with reset link
            // 3. For now, we'll just return true
            
            // TODO: Implement email sending and token storage
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // Validate token (in real app, check database)
            if (!await ValidateResetTokenAsync(dto.Token, dto.Email))
                return false;

            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == dto.Email);
            
            if (user == null)
                return false;

            // Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _userRepository.UpdateAsync(user.Id,user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                return false;

            // Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _userRepository.UpdateAsync(user.Id , user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ValidateResetTokenAsync(string token, string email)
        {
            // In a real application, you would:
            // 1. Check if token exists in database
            // 2. Check if token is not expired
            // 3. Check if token matches the email
            
            // For now, we'll implement a simple validation
            return !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(email);
        }

        private string GenerateResetToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}

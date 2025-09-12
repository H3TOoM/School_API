using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IPasswordResetService _passwordResetService;

        public PasswordResetController(IPasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _passwordResetService.ForgotPasswordAsync(dto);
            if (!result)
                return NotFound("Email not found");

            return Ok(new { message = "Password reset email sent" });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _passwordResetService.ResetPasswordAsync(dto);
            if (!result)
                return BadRequest("Invalid token or email");

            return Ok(new { message = "Password reset successfully" });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("NameIdentifier")?.Value ?? "0");
            if (userId == 0)
                return Unauthorized();

            var result = await _passwordResetService.ChangePasswordAsync(userId, dto);
            if (!result)
                return BadRequest("Current password is incorrect");

            return Ok(new { message = "Password changed successfully" });
        }
    }
}

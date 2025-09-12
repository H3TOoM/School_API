using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendEmailAsync(emailDto);
            if (!result)
                return BadRequest("Failed to send email");

            return Ok(new { message = "Email sent successfully" });
        }

        [HttpPost("notification")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationEmailDto notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendNotificationAsync(notificationDto);
            if (!result)
                return BadRequest("Failed to send notification");

            return Ok(new { message = "Notification sent successfully" });
        }
    }
}

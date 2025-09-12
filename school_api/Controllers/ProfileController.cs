using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = int.Parse(User.FindFirst("NameIdentifier")?.Value ?? "0");
            if (userId == 0)
                return Unauthorized();

            var profile = await _profileService.GetUserProfileAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("NameIdentifier")?.Value ?? "0");
            if (userId == 0)
                return Unauthorized();

            var result = await _profileService.UpdateUserProfileAsync(userId, dto);
            if (!result)
                return BadRequest("Failed to update profile");

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpGet("stats")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _profileService.GetUserStatsAsync();
            if (stats == null)
                return NotFound();

            return Ok(stats);
        }
    }
}

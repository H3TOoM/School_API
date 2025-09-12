using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenService.RefreshTokenAsync(dto.RefreshToken);
            if (result == null)
                return Unauthorized("Invalid refresh token");

            return Ok(result);
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenService.RevokeTokenAsync(dto.RefreshToken);
            if (!result)
                return BadRequest("Invalid refresh token");

            return Ok(new { message = "Token revoked successfully" });
        }
    }
}

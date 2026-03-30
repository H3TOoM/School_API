using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        public AccountController( IAccountService accountService, ITokenService tokenService )
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }


        [HttpPost( "Register" )]
        [AllowAnonymous]
        public async Task<IActionResult> Register( UserCreateDto dto )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _accountService.RegisterAsync( dto );
            var tokenPair = await _tokenService.GenerateTokenPairAsync(user);

            // Keep "Token" for backward compatibility
            return Ok(new { Token = tokenPair.AccessToken, tokenPair.RefreshToken, tokenPair.ExpiresAt, User = user });
        }

        [HttpPost( "Login" )]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginUserDto loginUser )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var user = await _accountService.LoginAsync( loginUser );
            var tokenPair = await _tokenService.GenerateTokenPairAsync(user);
            // Keep "Token" for backward compatibility
            return Ok(new { Token = tokenPair.AccessToken, tokenPair.RefreshToken, tokenPair.ExpiresAt, User = user });

        }

        [HttpDelete( "{id}" )]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount( int id )
        {
            var result = await _accountService.DeleteUserAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

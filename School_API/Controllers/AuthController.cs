using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School_API.DTOs;
using School_API.Services.Base;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user =await _authService.Register(dto);
            var token =await _tokenService.CreateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.RoleId,
                }
            });


        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.Login(dto);
            var token = await _tokenService.CreateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.RoleId,
                }
            });

        }






















    }
}

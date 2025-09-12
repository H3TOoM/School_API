using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using school_api.DTOs;
using school_api.Services;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || !users.Any())
                return NotFound( "No Users Yet!" );

            return Ok( users );
        }


        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetUserById( int id )
        {
            var user = await _userService.GetUserByIdAsync( id );
            if (user == null)
                return NotFound( "User Not Found!" );

            return Ok( user );
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser( UserCreateDto dto )
        {

            if (dto.Equals( null ))
                return BadRequest();

            var user = await _userService.CreateUserAsync( dto );

            return CreatedAtAction( nameof( GetUserById ), new { id = user.Id }, user );
        }


        [HttpPut( "{id}" )]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateUser( int id, [FromBody] UserUpdateDto dto )
        {
            var updatedUser = await _userService.UpdateUserAsync( id, dto );
            return Ok( updatedUser );
        }


        [HttpDelete( "{id}" )]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _userService.DeleteUserAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}

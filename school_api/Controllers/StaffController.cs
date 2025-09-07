using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllStaffes()
        {
            var staffes = await _staffService.GetAllStaffAsync();

            if (staffes == null || !staffes.Any())
                return NotFound( "No Staffes Yet!" );

            return Ok( staffes );
        }


        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetStaffById( int id )
        {
            var staffe = await _staffService.GetStaffByIdAsync( id );
            if (staffe == null)
                return NotFound( "staffe Not Found!" );

            return Ok( staffe );
        }


        [HttpPost]
        public async Task<IActionResult> CreateStaff( StaffCreateDto dto )
        {

            if (dto.Equals( null ))
                return BadRequest();

            var staff = await _staffService.CreateStaffAsync( dto );
          
            return CreatedAtAction( nameof( GetStaffById ), new { id = staff.Id }, staff );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateStaff( int id, [FromBody] StaffUpdateDto dto )
        {
            
            var staff = await _staffService.UpdateStaffAsync( id, dto );
            return Ok( staff );
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteStaff( int id )
        {
            var result = await _staffService.DeleteStaffAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}

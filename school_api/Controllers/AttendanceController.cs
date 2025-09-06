using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController( IAttendanceService attendanceService )
        {
            _attendanceService = attendanceService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            if (attendances == null || !attendances.Any())
                return NotFound();

            return Ok( attendances );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetAttendanceById( int id )
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync( id );
            if (attendance == null)
                return NotFound();


            return Ok( attendance );
        }


        [HttpPost]
        public async Task<IActionResult> CreateAttendance( AttendanceCreateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var attendance = await _attendanceService.CreateAttendance( dto );

            return CreatedAtAction( nameof( GetAttendanceById ), new { id = attendance.Id }, attendance );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateAttendance( int id, [FromBody] AttendanceUpdateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var updatedAttendance = await _attendanceService.UpdateAttendanceAsync( id, dto );
            return Ok( updatedAttendance );
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteAttendance( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _attendanceService.DeleteAttendanceAsync( id );

            if (!result)
                return NotFound();

            return NoContent();

        }

    }
}

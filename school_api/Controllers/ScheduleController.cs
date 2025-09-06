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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController( IScheduleService scheduleService )
        {
            _scheduleService = scheduleService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            if (schedules == null)
                return NotFound();

            return Ok( schedules );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetScheduleById( int id )
        {
            var schedule = await _scheduleService.GetSchedulesByIdAsync( id );
            if (schedule == null)
                return NotFound();

            return Ok( schedule );
        }


        [HttpPost]
        public async Task<IActionResult> CreateSchedule( ScheduleCreateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var schedule = await _scheduleService.CreateScheduleAsync( dto );
            return CreatedAtAction( nameof( GetScheduleById ), new { id = schedule.Id }, schedule );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateSchedule( int id, [FromBody] ScheduleUpdateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var updatedSchedule = await _scheduleService.UpdateScheduleAsync( id, dto );
            return Ok( updatedSchedule );
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteSchedule( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _scheduleService.DeleteScheduleAsync( id );

            if (!result)
                return NotFound();

            return NoContent();
        }




    }
}

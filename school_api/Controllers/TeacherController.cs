using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        // inject service
        private readonly ITeacherService _teacherService;
        public TeacherController( ITeacherService teacherService )
        {
            _teacherService = teacherService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            if ( teachers == null  || !teachers.Any())
                return NotFound();

            return Ok(teachers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id )
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if(teacher == null) return NotFound();
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherCreateDto dto )
        {
            if(dto.Equals(null)) 
                return BadRequest();

            var teacher = await _teacherService.CreateTeacherAsync(dto);
            return CreatedAtAction( nameof( GetTeacherById ), new { id = teacher.Id }, teacher );
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var teacher = await _teacherService.UpdateTeacherAsync(id,dto);
            return Ok(teacher);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (id == 0)
                return BadRequest();

            var result = await _teacherService.DeleteTeacherAsync( id );
            if(!result)
                return NotFound();

            return NoContent();
        }

    }
}

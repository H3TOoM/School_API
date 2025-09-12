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
    public class StudentClassController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;
        public StudentClassController( IStudentClassService studentClassService )
        {
            _studentClassService = studentClassService;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllStudentClasses()
        {
            var studentClasses = await _studentClassService.GetAllStudentClassesAsync();
            if (studentClasses == null || !studentClasses.Any())
                return NotFound("No Classes Yet!");

            return Ok( studentClasses );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetStudentClassById( int id )
        {
            var studentClass = await _studentClassService.GetStudentClassByIdAsync( id );
            if (studentClass == null)
                return NotFound( "Class Not Found!" );

            return Ok( studentClass );
        }


        [HttpPost]
        public async Task<IActionResult> CreateStudentClass( StudentClassCreateDto dto )
        {

            if (dto.Equals( null ))
                return BadRequest();

            var studentClass = await _studentClassService.CreateStudentClassAsync( dto );

            return CreatedAtAction( nameof( GetStudentClassById), new { id = studentClass.Id }, studentClass );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateStudentClass( int id, [FromBody] StudentClassUpdateDto dto )
        {
            var updatedStudentClass = await _studentClassService.UpdateStudentClassAsync( id, dto );
            return Ok( updatedStudentClass );
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteStudentClass( int id )
        {
            if(id == 0 )
                return BadRequest();

            var result = await _studentClassService.DeleteStudentClassAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }



    }
}

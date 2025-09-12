using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        // Inject Servcie
        private readonly IStudentService _studentService;
        public StudentController( IStudentService studentService )
        {
            _studentService = studentService;
        }

        // Get All Student
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            if ( students == null ) 
                return NotFound();

            return Ok( students );
        }

        // Get Student By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id )
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if ( student == null )
                return NotFound();

            return Ok( student );
        }


        // Create Student
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateStudent(StudentCreateDto dto )
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _studentService.CreateStudentAsync(dto);
            if(student == null)
                return NotFound();

            return CreatedAtAction( nameof( GetStudentById ), new { id = student.Id }, student );
        }

        // update student 
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStudent(int id , [FromBody] StudentUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var student = await _studentService.UpdateStudentAsync(id, dto);
            return Ok( student );
        }


        // Delete student
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id )
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if(!result)
                return NotFound();

            return NoContent();
        }



    }
}

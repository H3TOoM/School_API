using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
           _subjectService = subjectService; 
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            if (subjects == null || !subjects.Any())
                return NotFound( "No Subjects Yet!" );

            return Ok( subjects );
        }


        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetSubjectById( int id )
        {
            var subject = await _subjectService.GetSubjectByIdAsync( id );
            if (subject == null)
                return NotFound( "Subject Not Found!" );

            return Ok( subject );
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubject( SubjectCreateDto dto )
        {

            if (dto.Equals( null ))
                return BadRequest();

            var subject = await _subjectService.CreateSubjectAsync( dto );

            return CreatedAtAction( nameof( GetSubjectById ), new { id = subject.Id }, subject );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateSubject( int id, [FromBody] SubjectUpdateDto dto )
        {
            var updatedSubject = await _subjectService.UpdateSubjectAsync( id, dto );
            return Ok( updatedSubject );
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteSubject( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _subjectService.DeleteSubjectAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }




    }
}

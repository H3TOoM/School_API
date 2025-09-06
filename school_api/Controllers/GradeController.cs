using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            if (grades == null || !grades.Any())
                return NotFound("No Grades Yet!");

            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id )
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound( "Grade Not Found!" );

            return Ok(grade);
        }


        [HttpPost]
        public async Task<IActionResult> CreateGrade(GradeCreateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var grade = await _gradeService.CreateGradeAsync( dto );
            return CreatedAtAction( nameof( GetGradeById ), new { id = grade.Id }, grade );

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id,GradeUpdateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var updateGrade = await _gradeService.UpdateGradeAsync(id , dto );
            return Ok( updateGrade );
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id )
        {
            if(id == 0)
                return BadRequest();

            var result = await _gradeService.DeleteGradeAsync(id);
            if(!result)
                return NotFound();

            return NoContent();
        }



    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController( ICourseService courseService )
        {
            _courseService = courseService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();

            if (courses == null || !courses.Any())
                return NotFound( "No Courses Yet!" );

            return Ok( courses );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetCourseById( int id )
        {
            var course = await _courseService.GetCourseByIdAsync( id );
            if (course == null)
                return NotFound( "Course Not Found!" );

            return Ok( course );
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse( CourseCreateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var course = await _courseService.CreateCourseAsync( dto );
            return CreatedAtAction( nameof( GetCourseById ), new { id = course.Id }, course );

        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateCourse( int id, CourseUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var updatedCourse = await _courseService.UpdateCourseAsync( id, dto );
            return Ok( updatedCourse );
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteCourse( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _courseService.DeleteCourseAsync( id );
            if (!result)
                return NotFound();

            return NoContent();

        }



    }
}

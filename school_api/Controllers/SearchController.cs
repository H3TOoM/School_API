using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("students")]
        public async Task<IActionResult> SearchStudents([FromBody] StudentSearchFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _searchService.SearchStudentsAsync(filter);
            return Ok(result);
        }

        [HttpPost("teachers")]
        public async Task<IActionResult> SearchTeachers([FromBody] TeacherSearchFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _searchService.SearchTeachersAsync(filter);
            return Ok(result);
        }

        [HttpPost("courses")]
        public async Task<IActionResult> SearchCourses([FromBody] CourseSearchFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _searchService.SearchCoursesAsync(filter);
            return Ok(result);
        }

        [HttpPost("attendances")]
        public async Task<IActionResult> SearchAttendances([FromBody] AttendanceSearchFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _searchService.SearchAttendancesAsync(filter);
            return Ok(result);
        }
    }
}

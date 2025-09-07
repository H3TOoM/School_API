using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController( IDepartmentService departmentService )
        {
            _departmentService = departmentService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            if (departments == null || !departments.Any())
                return NotFound();

            return Ok( departments );
        }


        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetDepartmentById( int id )
        {
            var department = await _departmentService.GetDepartmentByIdAsync( id );
            if (department == null)
                return NotFound();

            return Ok( department );
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepaetment( DepartmentCreateDto dto )
        {
            if(dto.Equals(null))
                return BadRequest();

            var department = await _departmentService.CreateDepartmentAsync( dto );
            return CreatedAtAction( nameof( GetDepartmentById ), new { id = department.Id }, department );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateDepartment( int id, [FromBody] DepartmentUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var updatedDepartment = await _departmentService.UpdateDepartmentAsync( id, dto );
            return Ok( updatedDepartment );
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteDepartment( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _departmentService.DeleteDepartmentAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }




    }
}

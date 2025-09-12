using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class ParentController : ControllerBase
    {
        // Inject Service
        private readonly IParentService _parentService;
        public ParentController( IParentService parentService )
        {
            _parentService = parentService;
        }


        // Get All Parents 
        [HttpGet]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentService.GetAllParentsAsync();
            if (parents == null || !parents.Any())
                return NotFound();

            return Ok( parents );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetParentById( int id )
        {
            var parent = await _parentService.GetParentByIdAsync( id );
            if (parent == null)
                return NotFound();
            return Ok( parent );
        }


        // Create Parent 
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateParent( ParentCreateDto dto )
        {
            if (dto.Equals( null ))
                return BadRequest();

            var parent = await _parentService.CreateParentAsync( dto );
            return CreatedAtAction( nameof( GetParentById ), new { id = parent.Id }, parent );
        }

        // Update Parent 
        [HttpPut( "{id}" )]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateParent( int id, [FromBody] ParentUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var updatedParent = await _parentService.UpdateParentAsync( id, dto );
            return Ok( updatedParent );
        }


        // Delete Parent 
        [HttpDelete( "{id}" )]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteParent( int id )
        {
            if (id == 0)
                return BadRequest();

            var result = await _parentService.DeleteParentAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }



    }
}

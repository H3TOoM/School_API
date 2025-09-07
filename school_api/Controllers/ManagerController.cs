using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController( IManagerService managerService )
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();

            if (managers == null || !managers.Any())
                return NotFound( "No Managers Yet!" );

            return Ok( managers );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetManagerById( int id )
        {
            if (id == 0)
                return BadRequest();

            var manager = await _managerService.GetManagerByIdAsync( id );
            if (manager == null)
                return NotFound();

            return Ok( manager );
        }


        [HttpPost]
        public async Task<IActionResult> CreateManager( ManagerCreateDto dto )
        {
            if (dto.Equals( null ))
                return NotFound();


            var manager = await _managerService.CreateManagerAsync( dto );
            return CreatedAtAction( nameof( GetManagerById ), new { id = manager.Id }, manager );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateManager( int id, ManagerUpdateDto dto )
        {
            if (id == 0)
                return BadRequest();

            var updatedManager = await _managerService.UpdateManagerAsync( id, dto );

            return Ok( updatedManager );

        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteManager( int id )
        {
            if (id == 0)
                return NotFound();

            var result = await _managerService.DeleteManagerAsync( id );
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

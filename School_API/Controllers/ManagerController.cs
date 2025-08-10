using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_API.DTOs;
using School_API.Models;
using School_API.Services.Base;
using System.Threading.Tasks;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerSevice;
        public ManagerController(IManagerService managerSevice)
        {
            _managerSevice = managerSevice;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetManagersAsync()
        {
            var managers = await _managerSevice.GetAllManagersAsync();

            return Ok(managers);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]

        public async Task<IActionResult> GetManagerById(int id)
        {
            var manager = await _managerSevice.GetManagerByIdAsync(id);
            if (manager == null)
                return NotFound($"Manager with Id {id} not found.");

            return Ok(manager);
        }


        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> CreateManagerAsync(ManagerDto dto)
        {
            await _managerSevice.CreateManagerAsync(dto);

            return CreatedAtAction(nameof(GetManagerById), new { id = dto.Id }, dto);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]

        public async Task<IActionResult> UpdateManagerAsync(int id, Manager manager)
        {

            await _managerSevice.UpdateManagerAsync(id, manager);

            return Ok(manager);

        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> DeleteManagerAsync(int id)
        {
            await _managerSevice.DeleteManagerAsync(id);
            return Ok("Manager Has Been Deleted Successfully");
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.DTOs;
using school_api.Services.Base;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost("logs")]
        public async Task<IActionResult> GetAuditLogs([FromBody] AuditLogFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auditService.GetAuditLogsAsync(filter);
            return Ok(result);
        }
    }
}

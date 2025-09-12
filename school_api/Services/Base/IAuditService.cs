using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IAuditService
    {
        Task LogActionAsync(string action, string entityType, int? entityId, int userId, string? oldValues = null, string? newValues = null, string? ipAddress = null, string? userAgent = null);
        Task<PagedResultDto<AuditLogDto>> GetAuditLogsAsync(AuditLogFilterDto filter);
    }
}

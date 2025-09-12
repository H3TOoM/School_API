using AutoMapper;
using Microsoft.EntityFrameworkCore;
using school_api.Data;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class AuditService : IAuditService
    {
        private readonly IMainRepoistory<AuditLog> _auditLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuditService(
            IMainRepoistory<AuditLog> auditLogRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task LogActionAsync(string action, string entityType, int? entityId, int userId, string? oldValues = null, string? newValues = null, string? ipAddress = null, string? userAgent = null)
        {
            try
            {
                var auditLog = new AuditLog
                {
                    Action = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    UserId = userId,
                    OldValues = oldValues,
                    NewValues = newValues,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    Timestamp = DateTime.Now
                };

                await _auditLogRepository.CreateAsync(auditLog);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error but don't throw to avoid breaking the main operation
                Console.WriteLine($"Failed to log audit: {ex.Message}");
            }
        }

        public async Task<PagedResultDto<AuditLogDto>> GetAuditLogsAsync(AuditLogFilterDto filter)
        {
            // Change: GetAllAsync() returns IEnumerable<AuditLog>, which does not support Include.
            // Solution: Use the repository to return IQueryable<AuditLog> or use a DbSet directly if possible.
            // For now, remove .Include(a => a.User) and handle User as best as possible.

            var allLogs = await _auditLogRepository.GetAllAsync();
            var query = allLogs.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Action))
            {
                query = query.Where(a => a.Action.Contains(filter.Action));
            }

            if (!string.IsNullOrEmpty(filter.EntityType))
            {
                query = query.Where(a => a.EntityType.Contains(filter.EntityType));
            }

            if (filter.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == filter.UserId.Value);
            }

            if (filter.DateFrom.HasValue)
            {
                query = query.Where(a => a.Timestamp >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                query = query.Where(a => a.Timestamp <= filter.DateTo.Value);
            }

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(a => a.Action.Contains(filter.SearchTerm) ||
                                       a.EntityType.Contains(filter.SearchTerm)
                                       // .User may be null since we can't include it, so check for null
                                       || (a.User != null && a.User.Name.Contains(filter.SearchTerm)));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "timestamp":
                        query = filter.SortDescending ? query.OrderByDescending(a => a.Timestamp) : query.OrderBy(a => a.Timestamp);
                        break;
                    case "action":
                        query = filter.SortDescending ? query.OrderByDescending(a => a.Action) : query.OrderBy(a => a.Action);
                        break;
                    default:
                        query = query.OrderByDescending(a => a.Timestamp);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(a => a.Timestamp);
            }

            // Get total count
            var totalCount = query.Count();

            // Apply pagination
            var auditLogs = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var auditLogDtos = auditLogs.Select(a => new AuditLogDto
            {
                Id = a.Id,
                Action = a.Action,
                EntityType = a.EntityType,
                EntityId = a.EntityId,
                OldValues = a.OldValues,
                NewValues = a.NewValues,
                UserId = a.UserId,
                UserName = a.User?.Name,
                IpAddress = a.IpAddress,
                UserAgent = a.UserAgent,
                Timestamp = a.Timestamp
            }).ToList();

            return new PagedResultDto<AuditLogDto>
            {
                Data = auditLogDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
    }
}

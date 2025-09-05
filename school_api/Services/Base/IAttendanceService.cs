using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceReadDto>> GetAllAttendancesAsync();
        Task<AttendanceReadDto> GetAttendanceByIdAsync(int id);

        Task<AttendanceReadDto> CreateAttendance(AttendanceCreateDto dto);
        Task<AttendanceReadDto> UpdateAttendanceAsync(int  id, AttendanceUpdateDto dto);

        Task<bool> DeleteAttendanceAsync(int id);
    }
}

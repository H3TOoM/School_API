using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleReadDto>> GetAllSchedulesAsync();
        Task<ScheduleReadDto> GetSchedulesByIdAsync( int id );

        Task<ScheduleReadDto> CreateScheduleAsync(ScheduleCreateDto dto);

        Task<ScheduleReadDto> UpdateScheduleAsync(int id , ScheduleUpdateDto dto);

        Task<bool> DeleteScheduleAsync(int id);
    }
}

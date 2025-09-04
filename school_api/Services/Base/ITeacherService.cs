using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherReadDto>> GetAllTeachersAsync();
        Task<TeacherReadDto> GetTeacherByIdAsync(int id);

        Task<TeacherReadDto> CreateTeacherAsync( TeacherCreateDto dto );

        Task<TeacherReadDto> UpdateTeacherAsync(int id, TeacherUpdateDto dto );

        Task<bool> DeleteTeacherAsync(int id);
    }
}

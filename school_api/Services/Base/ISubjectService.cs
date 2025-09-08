using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectReadDto>> GetAllSubjectsAsync();
        Task<SubjectReadDto> GetSubjectByIdAsync(int id);
        Task<SubjectReadDto> CreateSubjectAsync(SubjectCreateDto dto);

        Task<SubjectReadDto> UpdateSubjectAsync(int id ,SubjectUpdateDto dto);

        Task<bool> DeleteSubjectAsync(int id);
    }
}

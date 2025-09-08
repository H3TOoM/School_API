using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IStudentClassService
    {
        Task<IEnumerable<StudentClassReadDto>> GetAllStudentClassesAsync();
        Task<StudentClassReadDto> GetStudentClassByIdAsync( int id );
        Task<StudentClassReadDto> CreateStudentClassAsync( StudentClassCreateDto dto );

        Task<StudentClassReadDto> UpdateStudentClassAsync( int id, StudentClassUpdateDto dto );

        Task<bool> DeleteStudentClassAsync( int id );
    }
}

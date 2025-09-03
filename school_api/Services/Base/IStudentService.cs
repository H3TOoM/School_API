using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync();

        Task<StudentReadDto> GetStudentByIdAsync(int id);  

        Task<StudentReadDto> CreateStudentAsync(StudentCreateDto dto);

        Task<StudentReadDto> UpdateStudentAsync(int id ,StudentUpdateDto dto);

        Task<bool> DeleteStudentAsync(int id);
    }
}

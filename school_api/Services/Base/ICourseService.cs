using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseReadDto>> GetAllCoursesAsync();
        Task<CourseReadDto> GetCourseByIdAsync( int id );

        Task<CourseReadDto> CreateCourseAsync( CourseCreateDto dto );

        Task<CourseReadDto> UpdateCourseAsync( int id, CourseUpdateDto dto );

        Task<bool> DeleteCourseAsync( int id );
    }
}

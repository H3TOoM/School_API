using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface ISearchService
    {
        Task<PagedResultDto<StudentReadDto>> SearchStudentsAsync(StudentSearchFilterDto filter);
        Task<PagedResultDto<TeacherReadDto>> SearchTeachersAsync(TeacherSearchFilterDto filter);
        Task<PagedResultDto<CourseReadDto>> SearchCoursesAsync(CourseSearchFilterDto filter);
        Task<PagedResultDto<AttendanceReadDto>> SearchAttendancesAsync(AttendanceSearchFilterDto filter);
    }
}

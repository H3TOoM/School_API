using System.ComponentModel.DataAnnotations;

namespace school_api.DTOs
{
    public class SearchFilterDto
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Dictionary<string, object>? Filters { get; set; }
    }

    public class StudentSearchFilterDto : SearchFilterDto
    {
        public int? DepartmentId { get; set; }
        public int? StudentClassId { get; set; }
        public int? ParentId { get; set; }
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
    }

    public class TeacherSearchFilterDto : SearchFilterDto
    {
        public int? DepartmentId { get; set; }
        public int? SubjectId { get; set; }
    }

    public class CourseSearchFilterDto : SearchFilterDto
    {
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentClassId { get; set; }
    }

    public class AttendanceSearchFilterDto : SearchFilterDto
    {
        public int? StudentId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Status { get; set; }
    }
}

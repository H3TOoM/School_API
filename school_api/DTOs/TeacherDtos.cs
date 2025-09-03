using System.Collections.Generic;

namespace school_api.DTOs
{
    public class TeacherReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }
        public IdNameDto Department { get; set; }
        public IdNameDto Subject { get; set; }
    }

    public class TeacherCreateDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int SubjectId { get; set; }
    }

    public class TeacherUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }
        public int? SubjectId { get; set; }
    }
}


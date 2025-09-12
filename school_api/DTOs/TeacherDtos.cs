using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentId { get; set; }
        
        [Required(ErrorMessage = "Subject ID is required")]
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


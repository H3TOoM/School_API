using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace school_api.DTOs
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int UserId { get; set; }
        public IdNameDto? Parent { get; set; }
        public IdNameDto StudentClass { get; set; } = new IdNameDto();
    }

    public class StudentCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        
        public int? ParentId { get; set; }
        
        [Required(ErrorMessage = "Student class ID is required")]
        public int StudentClassId { get; set; }
    }

    public class StudentUpdateDto
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? ParentId { get; set; }
        public int? StudentClassId { get; set; }
    }
}


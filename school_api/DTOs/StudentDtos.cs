using System;
using System.Collections.Generic;

namespace school_api.DTOs
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int UserId { get; set; }
        public IdNameDto? Parent { get; set; }
        public IdNameDto StudentClass { get; set; }
    }

    public class StudentCreateDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int UserId { get; set; }
        public int ParentId { get; set; }
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


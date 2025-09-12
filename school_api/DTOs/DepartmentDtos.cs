using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace school_api.DTOs
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IdNameDto Manager { get; set; }
    }

    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Manager ID is required")]
        public int ManagerId { get; set; }
    }

    public class DepartmentUpdateDto
    {
        public string? Name { get; set; }
        public int? ManagerId { get; set; }
    }
}


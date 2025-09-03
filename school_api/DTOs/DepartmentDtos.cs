using System.Collections.Generic;

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
        public string Name { get; set; }
        public int ManagerId { get; set; }
    }

    public class DepartmentUpdateDto
    {
        public string? Name { get; set; }
        public int? ManagerId { get; set; }
    }
}


namespace school_api.DTOs
{
    public class StudentClassReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IdNameDto Department { get; set; }
        public IdNameOptionalDto? Teacher { get; set; }
    }

    public class StudentClassCreateDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int? TeacherId { get; set; }
    }

    public class StudentClassUpdateDto
    {
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public int? TeacherId { get; set; }
    }
}


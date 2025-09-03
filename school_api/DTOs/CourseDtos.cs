namespace school_api.DTOs
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        public IdNameDto Subject { get; set; } = default!;
        public IdNameOptionalDto? Teacher { get; set; }
    }

    public class CourseCreateDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        public int SubjectId { get; set; }
        public int? TeacherId { get; set; }
    }

    public class CourseUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
    }
}


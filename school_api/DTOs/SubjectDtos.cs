namespace school_api.DTOs
{
    public class SubjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class SubjectCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class SubjectUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}


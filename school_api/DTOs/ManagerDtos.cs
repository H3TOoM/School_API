namespace school_api.DTOs
{
    public class ManagerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class ManagerCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class ManagerUpdateDto
    {
        public string? Name { get; set; }
    }
}


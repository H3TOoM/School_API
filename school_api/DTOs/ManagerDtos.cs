namespace school_api.DTOs
{
    public class ManagerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }

    public class ManagerCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }

    public class ManagerUpdateDto
    {
        public string? Name { get; set; }
    }
}


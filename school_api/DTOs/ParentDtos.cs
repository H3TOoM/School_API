using System.Collections.Generic;

namespace school_api.DTOs
{
    public class ParentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }

    public class ParentCreateDto
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }

    public class ParentUpdateDto
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}


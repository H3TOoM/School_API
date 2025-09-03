namespace school_api.DTOs
{
    // Small nested read-only DTOs to embed in larger reads
    public class IdNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IdNameOptionalDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}


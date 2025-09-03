namespace school_api.DTOs
{
    public class StaffReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }
        public IdNameDto Department { get; set; }
    }

    public class StaffCreateDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class StaffUpdateDto
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public double? Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }
    }
}


namespace school_api.DTOs
{
    public class GradeReadDto
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int StudentId { get; set; }
        public int CourserId { get; set; }
    }

    public class GradeCreateDto
    {
        public double Score { get; set; }
        public int StudentId { get; set; }
        public int CourserId { get; set; }
    }

    public class GradeUpdateDto
    {
        public double? Score { get; set; }
    }
}


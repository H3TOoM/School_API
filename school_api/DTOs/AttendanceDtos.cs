using System;

namespace school_api.DTOs
{
    public class AttendanceReadDto
    {
        public int Id { get; set; }
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
    }

    public class AttendanceCreateDto
    {
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
    }

    public class AttendanceUpdateDto
    {
        public bool? IsPresent { get; set; }
        public DateTime? Date { get; set; }
    }
}


using System;

namespace school_api.DTOs
{
    public class ScheduleReadDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public IdNameDto Course { get; set; }
        public IdNameDto Teacher { get; set; }
        public IdNameDto StudentClass { get; set; }
    }

    public class ScheduleCreateDto
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int StudentClassId { get; set; }
    }

    public class ScheduleUpdateDto
    {
        public DayOfWeek? Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentClassId { get; set; }
    }
}


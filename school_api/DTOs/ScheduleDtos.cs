using System;

namespace school_api.DTOs
{
    public class ScheduleReadDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public IdNameDto Course { get; set; } = new IdNameDto();
        public IdNameDto Teacher { get; set; } = new IdNameDto();
        public IdNameDto StudentClass { get; set; } = new IdNameDto();
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


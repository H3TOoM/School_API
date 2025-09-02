using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DayOfWeek Day { get; set; }   

        [Required]
        public TimeSpan StartTime { get; set; }  

        [Required]
        public TimeSpan EndTime { get; set; }    

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [Required]
        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }

}

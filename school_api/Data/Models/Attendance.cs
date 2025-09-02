
using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsPresent { get; set; } 

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public DateTime Date { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
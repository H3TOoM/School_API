using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Score { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
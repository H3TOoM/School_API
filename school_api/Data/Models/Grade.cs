using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range( 0, 100 )]
        public double Score { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int CourserId { get; set; }
        public Course Course { get; set; }
    }
}

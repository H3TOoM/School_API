using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public int SubjectId { get; set; }      
        public Subject Subject { get; set; } = null!;


        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

    }
}

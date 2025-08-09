using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class SchoolClass
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        [Required]
        public Schedule Schedule { get; set; }
    }
}
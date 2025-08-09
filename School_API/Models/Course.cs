using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        public int ClassId { get; set; }
        public SchoolClass Class { get; set; }

        [Required]
        public int ParentId { get; set; }
        public Parent Parent { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}

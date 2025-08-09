using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Teacher
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
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<SchoolClass> Classes { get; set; } = new List<SchoolClass>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

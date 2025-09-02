using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }


        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; }



        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Course> Courses { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [StringLength( 100 )]
        public string? Email { get; set; }

        [StringLength( 20 )]
        public string? PhoneNumber { get; set; }



        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; } = null!;


        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }

        [Required]
        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; } = null!;

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }


}

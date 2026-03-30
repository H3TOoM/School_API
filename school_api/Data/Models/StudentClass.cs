using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class StudentClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 50 )]
        public string Name { get; set; } = string.Empty;  

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

}

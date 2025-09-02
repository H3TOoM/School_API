using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Staff>? StaffMembers { get; set; }


        [Required]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public ICollection<Teacher>? Teachers { get; set; }

        public ICollection<StudentClass>? Classes { get; set; }

    }
}

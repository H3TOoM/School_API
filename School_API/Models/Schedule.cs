using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string DayOfWeek { get; set; }

        [Required, StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public int ClassId { get; set; }
        public SchoolClass Class { get; set; }
    }
}
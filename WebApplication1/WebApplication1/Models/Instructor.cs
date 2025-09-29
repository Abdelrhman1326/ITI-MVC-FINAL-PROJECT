using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        // dept id foreign key:
        public int DeptId { get; set; }
        // navigation property:
        public Department? Department { get; set; }
        // crs id:
        public int CrsId { get; set; }
        // navigation property:
        public Course? Course { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Degree { get; set; }
        public int MinimumDegree { get; set; }
        [Precision(10, 2)]
        public decimal Hours { get; set; }

        // Foreign key to Department
        public int DeptId { get; set; }

        // Navigation property (one department → many courses)
        public Department? Department { get; set; }

        // Navigation property (one course → many instructors)
        public ICollection<Instructor>? Instructors { get; set; }
    }
}
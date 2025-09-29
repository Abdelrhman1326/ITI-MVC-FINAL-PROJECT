using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        // every student is assigned to one departemnt "foreign key"
        // every student has many courseStudent
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Address { get; set; }
        public int Grade { get; set; }

        // Foreign key: Department
        public int DeptId { get; set; }
        // navigation property:
        public Department? Department { get; set; }
        // navigation: student maybe in many courses
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
    }
}
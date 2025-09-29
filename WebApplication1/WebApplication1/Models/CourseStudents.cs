namespace WebApplication1.Models
{
    public class CourseStudents
    {
        // Composite Key (CourseId + StudentId)
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        // Navigation properties
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
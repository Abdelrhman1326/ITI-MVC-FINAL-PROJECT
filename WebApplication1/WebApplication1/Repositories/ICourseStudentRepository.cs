using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ICourseStudentRepository
{
    public List<Course> GetAllCourses();
    public List<Student> GetAllStudents();
    public List<Student> GetStudentsByCourseId(int id);
    public bool EnrollStudent(int courseId, int studentId);
    bool UnenrollStudent(int courseId, int studentId); 
    public string GetCourseName(int courseId);
}
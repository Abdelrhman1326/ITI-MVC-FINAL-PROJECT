using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ICourseStudentRepository
{
    public List<Course> GetAllCourses();
    public List<Student> GetStudentsByCourseId(int id);
}
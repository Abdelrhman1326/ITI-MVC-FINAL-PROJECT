using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Repositories;

public class CourseStudentRepository : ICourseStudentRepository
{
    private readonly ApplicationDbContext _context;

    public CourseStudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // method to get all courses
    public List<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }

    // method to get all students assigned to course
    public List<Student> GetStudentsByCourseId(int id)
    {
        List<int> stududentsIdList = _context.CourseStudents
            .Where(x => x.CourseId == id)
            .Select(x => x.StudentId)
            .ToList();
        // create a list of students with selected ids:
        List<Student> students = _context.Students
            .Where(s => stududentsIdList.Contains(s.StudentId))
            .ToList();
        return students;
    }
}
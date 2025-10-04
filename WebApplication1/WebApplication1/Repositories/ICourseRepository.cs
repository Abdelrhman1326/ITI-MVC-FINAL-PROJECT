using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories;

public interface ICourseRepository
{
    IEnumerable<Course> GetCourses();
    Course GetCourse(int id);
    void CreateCourse(Course course);
    void EditCourse(Course course);
    void DeleteCourse(int id);
    void Save();
}
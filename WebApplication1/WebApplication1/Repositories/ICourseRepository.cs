using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories;

public interface ICourseRepository
{
    public IEnumerable<Course> GetCourses();
    public Course GetCourse(int id);
    public void CreateCourse(Course course);
    public void EditCourse(Course course);
    public void DeleteCourse(int id);
    public void Save();
}
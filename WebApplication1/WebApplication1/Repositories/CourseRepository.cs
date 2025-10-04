using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;
    
    // Constructor injection:
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Course> GetCourses()
    {
        return _context.Courses.ToList(); 
    }
    
    public Course GetCourse(int id)
    {
        return _context.Courses.Find(id);
    }
    
    public void CreateCourse(Course course)
    {
        _context.Courses.Add(course);
    }
    
    public void EditCourse(Course course)
    {
        _context.Courses.Update(course);
    }

    public void DeleteCourse(int id)
    {
        var courseToDelete = _context.Courses.Find(id);
        if (courseToDelete != null)
        {
            _context.Courses.Remove(courseToDelete);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
using WebApplication1.Data;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
    
    // method to get all available students:
    public List<Student> GetAllStudents()
    {
        return _context.Students.ToList();
    }

    // method to get all students assigned to course
    public List<Student> GetStudentsByCourseId(int id)
    {
        List<int> stududentsIdList = _context.CourseStudents
            .Where(x => x.CourseId == id)
            .Select(x => x.StudentId)
            .ToList();
        List<Student> students = _context.Students
            .Where(s => stududentsIdList.Contains(s.StudentId))
            .ToList();
        return students;
    }
    
    public bool EnrollStudent(int courseId, int studentId)
    {
        try 
        {
            var enrollment = new CourseStudents() 
            { 
                CourseId = courseId, 
                StudentId = studentId 
            };
            
            _context.CourseStudents.Add(enrollment);
            _context.SaveChanges();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error enrolling student: {ex.Message}");
            return false;
        }
    }

    public string GetCourseName(int courseId)
    {
        var course = _context.Courses.Find(courseId); 
    
        if (course == null)
        {
            return null; 
        }
    
        return course.Name;
    }
    
    public bool UnenrollStudent(int courseId, int studentId)
    {
        try
        {
            // 1. Find the specific CourseStudent association object
            var enrollment = _context.CourseStudents
                .FirstOrDefault(cs => cs.CourseId == courseId && cs.StudentId == studentId);

            if (enrollment == null)
            {
                return false; // Association not found
            }
        
            // 2. Remove the object
            _context.CourseStudents.Remove(enrollment);
            _context.SaveChanges();
        
            return true;
        }
        catch (Exception ex)
        {
            // Log error
            Console.WriteLine($"Error unenrolling student: {ex.Message}");
            return false;
        }
    }
}
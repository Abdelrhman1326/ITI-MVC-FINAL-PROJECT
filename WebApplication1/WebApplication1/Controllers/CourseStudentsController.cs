using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;
using System.Collections.Generic;

namespace WebApplication1.Controllers;

public class CourseStudentsController : Controller
{
    private readonly ICourseStudentRepository _repository; // This is the injected field

    public CourseStudentsController(ICourseStudentRepository repository)
    {
        _repository = repository;
    }

    // GET: /CourseStudents/Index -> returns a list of all available courses
    public IActionResult Index()
    {
        List<Course> courses = _repository.GetAllCourses();
        return View(courses);
    }

    // GET: /CourseStudents/Students/{id} -> returns students assigned to the passed course ID
    public IActionResult Students(int id)
    {
        // Pass the course ID to ViewData so the Students.cshtml view can use it for the form/heading
        ViewData["CourseId"] = id;
        string courseName = _repository.GetCourseName(id);
        ViewData["CourseName"] = courseName??"";
        
        List<Student> students = _repository.GetStudentsByCourseId(id);
        return View(students);
    }

    // POST: /CourseStudents/AddStudentToCourse
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddStudentToCourse(int courseId, int studentId)
    {
        // CORRECTION: Use the correctly named field: _repository
        bool success = _repository.EnrollStudent(courseId, studentId);

        if (success)
        {
            TempData["Message"] = "Student successfully enrolled!";
        }
        else
        {
            TempData["Error"] = "Enrollment failed. Student may be invalid or already enrolled.";
        }
        
        return RedirectToAction(nameof(Students), new { id = courseId }); 
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveStudentFromCourse(int courseId, int studentId)
    {
        if (courseId <= 0 || studentId <= 0)
        {
            TempData["Error"] = "Invalid Course ID or Student ID provided.";
            return RedirectToAction(nameof(Students), new { id = courseId });
        }
    
        // Call the repository method to perform the deletion
        bool success = _repository.UnenrollStudent(courseId, studentId); 

        if (success)
        {
            TempData["Message"] = $"Student ID {studentId} was successfully removed from the course.";
        }
        else
        {
            TempData["Error"] = "Failed to remove student. Association may not exist.";
        }
    
        // Redirect back to the updated Students list
        return RedirectToAction(nameof(Students), new { id = courseId }); 
    }
}
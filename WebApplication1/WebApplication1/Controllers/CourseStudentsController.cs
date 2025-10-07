using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class CourseStudentsController : Controller
{
    private readonly ICourseStudentRepository _repository;
    public CourseStudentsController(ICourseStudentRepository repository)
    {
        _repository = repository;
    }
    // GET -> index -> should return a list of all available courses
    public IActionResult Index()
    {
        List<Course> courses = _repository.GetAllCourses();
        return View(courses);
    }
    
    // GET INDEX/id -> return students assigned to the passesd id:
    public IActionResult Students(int id)
    {
        List<Student> students = _repository.GetStudentsByCourseId(id);
        return View(students);
    }
}
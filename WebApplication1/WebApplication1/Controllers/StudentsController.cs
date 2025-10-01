using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentRepository _studentRepository;
    
    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    // GET: /Students
    public IActionResult Index()
    {
        IEnumerable<Student> allStudents = _studentRepository.GetStudents();
        return View(allStudents);
    }

    public IActionResult Details(int id)
    {
        Student student = _studentRepository.GetStudent(id);
        return View(student);
    }
}
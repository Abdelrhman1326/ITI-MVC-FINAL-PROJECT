using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // GET: /Students/Edit/5
    public IActionResult Edit(int id)
    {
        var student = _studentRepository.GetStudent(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost]
    public IActionResult Edit(int id, Student student)
    {
        if (id != student.StudentId) 
        {
            return BadRequest(); 
        }

        if (ModelState.IsValid)
        {
            try
            {
                _studentRepository.EditStudent(student);
                _studentRepository.Save();
                
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_studentRepository.GetStudent(student.StudentId) == null)
                {
                    return NotFound();
                }
                throw; 
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Please ensure the data entered is valid and the Department ID exists.");
            }
        }
        
        return View(student);
    }
}
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

    // GET: /Students/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(); 
    }

    // POST: /Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _studentRepository.CreateStudent(student);
                _studentRepository.Save();
            
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }
        catch (Exception exp)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the student. Please check your data.");
            return View(student);
        }
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
    
    // POST: /Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
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

    // GET: /Students/Delete/5
    public IActionResult Delete(int id)
    {
        var student = _studentRepository.GetStudent(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: /Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = _studentRepository.GetStudent(id);
        if (student == null)
        {
            return RedirectToAction(nameof(Index));
        }
        _studentRepository.DeleteStudent(id);
        _studentRepository.Save();
        TempData["StatusMessage"] = $"Student with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
}
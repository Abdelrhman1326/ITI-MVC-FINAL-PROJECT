using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class StudentsController : Controller
{
    private readonly ICrudRepository<Student> _studentRepository;
    private readonly ICrudRepository<Department> _departmentRepository;
    
    public StudentsController(
        ICrudRepository<Student> studentRepository,
        ICrudRepository<Department> departmentRepository) 
    {
        _studentRepository = studentRepository;
        _departmentRepository = departmentRepository;
    }
    
    // GET: /Students
    public IActionResult Index()
    {
        IEnumerable<Student> allStudents = _studentRepository.GetAll();
        return View(allStudents);
    }

    public IActionResult Details(int id)
    {
        Student student = _studentRepository.GetOne(id);
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
        SetDepartmentSelectList(null);
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
                _studentRepository.Create(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            SetDepartmentSelectList(student.DeptId);
            return View(student);
        }
        catch (Exception exp)
        {
            SetDepartmentSelectList(student.DeptId);
            ModelState.AddModelError(string.Empty, "An error occurred while saving the student. Please check your data.");
            return View(student);
        }
    }

    // GET: /Students/Edit/5
    public IActionResult Edit(int id)
    {
        var student = _studentRepository.GetOne(id);
        if (student == null)
        {
            return NotFound();
        }
        SetDepartmentSelectList(student.DeptId);
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
                _studentRepository.Edit(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_studentRepository.GetOne(student.StudentId) == null)
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
        
        SetDepartmentSelectList(student.DeptId);
        return View(student);
    }

    // GET: /Students/Delete/5
    public IActionResult Delete(int id)
    {
        var student = _studentRepository.GetOne(id);
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
        var student = _studentRepository.GetOne(id);
        if (student == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        _studentRepository.Delete(id);
        _studentRepository.Save();
        TempData["StatusMessage"] = $"Student with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
    
    private void SetDepartmentSelectList(int? selectedId)
    {
        ViewData["Departments"] = new SelectList(
            _departmentRepository.GetAll(), 
            "Id", 
            "Name", 
            selectedId
        );
    }
}
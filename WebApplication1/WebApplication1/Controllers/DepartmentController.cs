using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class DepartmentController : Controller
{
    // Dependency injection of the generic repository specific to Department
    private readonly ICrudRepository<Department> _departmentRepository;

    public DepartmentController(ICrudRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    // GET: /Departments
    // Displays a list of all departments
    public IActionResult Index()
    {
        IEnumerable<Department> allDepartments = _departmentRepository.GetAll();
    
        if (allDepartments == null)
        {
            allDepartments = Enumerable.Empty<Department>();
        }
    
        return View(allDepartments);
    }
    
    // GET: /Departments/Details/5
    // Displays details for a single department
    public IActionResult Details(int id)
    {
        Department department = _departmentRepository.GetOne(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }

    // GET: /Departments/Create
    // Returns the view for creating a new department
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Departments/Create
    // Handles the form submission for creating a new department
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Department department)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Create(department);
                _departmentRepository.Save();
                // Redirect to the list view after successful creation
                return RedirectToAction(nameof(Index)); 
            }

            return View(department); // Return the view if input errors are found
        }
        catch (Exception exp)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the department. Please check your data.");
            return View(department);
        }
    }
    
    // GET: /Departments/Edit/5
    // Returns the view for editing an existing department
    public IActionResult Edit(int id)
    {
        var department = _departmentRepository.GetOne(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }
    
    // POST: /Departments/Edit/5
    // Handles the form submission for editing an existing department
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Department department)
    {
        // Security check: ensure the ID in the route matches the ID in the model
        if (id != department.Id) 
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _departmentRepository.Edit(department);
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            // Exception handling specific to EF Core updates
            catch (DbUpdateConcurrencyException)
            {
                if (_departmentRepository.GetOne(department.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Please ensure the data entered is valid and unique where required.");
            }
        }
        
        return View(department);
    }
    
    // GET: /Departments/Delete/5
    // Returns the confirmation view before deletion
    public IActionResult Delete(int id)
    {
        var department = _departmentRepository.GetOne(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }
    
    // POST: /Departments/Delete/5
    // Handles the actual deletion operation
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var department = _departmentRepository.GetOne(id);
        
        if (department == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        _departmentRepository.Delete(id);
        _departmentRepository.Save();
        
        // Optional: Provide feedback to the user upon success
        TempData["StatusMessage"] = $"Department with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
}
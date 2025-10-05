using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class InstructorController : Controller
{
    // Dependency injection of the generic repository specific to Instructor
    private readonly ICrudRepository<Instructor> _instructorRepository;

    public InstructorController(ICrudRepository<Instructor> instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }
    
    // GET: /Instructors
    // Displays a list of all instructors
    public IActionResult Index()
    {
        IEnumerable<Instructor> allInstructors = _instructorRepository.GetAll();
    
        if (allInstructors == null)
        {
            allInstructors = Enumerable.Empty<Instructor>();
        }
    
        return View(allInstructors);
    }
    
    // GET: /Instructors/Details/5
    // Displays details for a single instructor
    public IActionResult Details(int id)
    {
        // Repository is configured to eagerly load Department and Course
        Instructor instructor = _instructorRepository.GetOne(id);
        if (instructor == null)
        {
            return NotFound();
        }
        return View(instructor);
    }

    // GET: /Instructors/Create
    // Returns the view for creating a new instructor
    [HttpGet]
    public IActionResult Create()
    {
        // NOTE: In a real app, you'd pass SelectLists for DeptId and CrsId here.
        return View();
    }

    // POST: /Instructors/Create
    // Handles the form submission for creating a new instructor
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Instructor instructor)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Create(instructor);
                _instructorRepository.Save();
                // Redirect to the list view after successful creation
                return RedirectToAction(nameof(Index)); 
            }

            // NOTE: In a real app, you'd repopulate SelectLists here before returning the view.
            return View(instructor);
        }
        catch (Exception exp)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the instructor. Please check your data.");
            return View(instructor);
        }
    }
    
    // GET: /Instructors/Edit/5
    // Returns the view for editing an existing instructor
    public IActionResult Edit(int id)
    {
        var instructor = _instructorRepository.GetOne(id);
        if (instructor == null)
        {
            return NotFound();
        }
        // NOTE: In a real app, you'd pass SelectLists for DeptId and CrsId here.
        return View(instructor);
    }
    
    // POST: /Instructors/Edit/5
    // Handles the form submission for editing an existing instructor
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Instructor instructor)
    {
        if (id != instructor.Id) 
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _instructorRepository.Edit(instructor);
                _instructorRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_instructorRepository.GetOne(instructor.Id) == null)
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
        
        // NOTE: In a real app, you'd repopulate SelectLists here before returning the view.
        return View(instructor);
    }
    
    // GET: /Instructors/Delete/5
    // Returns the confirmation view before deletion
    public IActionResult Delete(int id)
    {
        var instructor = _instructorRepository.GetOne(id);
        if (instructor == null)
        {
            return NotFound();
        }
        return View(instructor);
    }
    
    // POST: /Instructors/Delete/5
    // Handles the actual deletion operation
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var instructor = _instructorRepository.GetOne(id);
        
        if (instructor == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        _instructorRepository.Delete(id);
        _instructorRepository.Save();
        
        TempData["StatusMessage"] = $"Instructor with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
}
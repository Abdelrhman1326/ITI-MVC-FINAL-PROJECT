using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class CourseController : Controller
{
    private readonly ICrudRepository<Course> _courseRepository;
    private readonly ICrudRepository<Instructor> _instructorRepository; 
    private readonly ICrudRepository<Department> _departmentRepository; 

    // Dependency Injection for all required repositories
    public CourseController(
        ICrudRepository<Course> courseRepository,
        ICrudRepository<Instructor> instructorRepository,
        ICrudRepository<Department> departmentRepository)
    {
        _courseRepository = courseRepository;
        _instructorRepository = instructorRepository;
        _departmentRepository = departmentRepository;
    }
    
    // GET: /Courses
    public IActionResult Index()
    {
        IEnumerable<Course> allCourses = _courseRepository.GetAll();
        if (allCourses == null)
        {
            allCourses = Enumerable.Empty<Course>();
        }
        return View(allCourses);
    }
    
    // GET: /Courses/Details
    public IActionResult Details(int id)
    {
        Course course = _courseRepository.GetOne(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // GET: /Courses/Create
    [HttpGet]
    public IActionResult Create()
    {
        // 1. Fetch and prepare Departments for dropdown
        ViewData["Departments"] = new SelectList(
            _departmentRepository.GetAll(), 
            "Id",                          
            "Name"                         
        );
        
        // 2. Fetch and prepare Instructors for dropdown
        ViewData["Instructors"] = new SelectList(
            _instructorRepository.GetAll(), 
            "Id",
            "Name"
        );
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Course course, [FromForm] int? selectedInstructorId)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // Logic to link the selected instructor (Many-to-Many)
                if (selectedInstructorId.HasValue)
                {
                    var instructor = _instructorRepository.GetOne(selectedInstructorId.Value);
                    
                    if (instructor != null)
                    {
                        if (course.Instructors == null)
                        {
                            course.Instructors = new List<Instructor>();
                        }
                        
                        // Attach the Instructor to the Course
                        course.Instructors.Add(instructor);
                    }
                }
                
                _courseRepository.Create(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, re-fetch SelectLists before returning view
            SetViewDataSelectLists(course.DeptId, selectedInstructorId);
            return View(course);
        }
        catch (Exception exp)
        {
            // If exception occurs, re-fetch SelectLists before returning view
            SetViewDataSelectLists(course.DeptId, selectedInstructorId);
            ModelState.AddModelError(string.Empty, $"An error occurred while saving the course: {exp.Message}");
            return View(course);
        }
    }
    
    // GET: /Courses/Edit/5
    // NOTE: For full functionality, this should also load Department/Instructor lists.
    public IActionResult Edit(int id)
    {
        var course = _courseRepository.GetOne(id);
        if (course == null)
        {
            return NotFound();
        }
        
        // Prepare SelectLists for the Edit view
        int? currentInstructorId = course.Instructors?.FirstOrDefault()?.Id;
        SetViewDataSelectLists(course.DeptId, currentInstructorId);
        
        return View(course);
    }
    
    // POST: /Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Course course)
    {
        if (id != course.Id) 
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // NOTE: Full Edit logic for Many-to-Many would require updating the Instructors collection here.
                _courseRepository.Edit(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_courseRepository.GetOne(course.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Please ensure the data entered is valid");
            }
        }
        
        // If validation/save fails, re-fetch SelectLists before returning view
        SetViewDataSelectLists(course.DeptId, null); // Cannot easily determine current instructor here
        return View(course);
    }
    
    // GET: /Courses/Delete/5
    public IActionResult Delete(int id)
    {
        var course = _courseRepository.GetOne(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }
    
    // POST: /Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var course = _courseRepository.GetOne(id);
        if (course == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        _courseRepository.Delete(id);
        _courseRepository.Save();
        TempData["StatusMessage"] = $"Course with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
    
    // Helper method to keep code clean and dry
    private void SetViewDataSelectLists(int? selectedDeptId, int? selectedInstructorId)
    {
        ViewData["Departments"] = new SelectList(
            _departmentRepository.GetAll(), 
            "Id", "Name", selectedDeptId
        );
        
        ViewData["Instructors"] = new SelectList(
            _instructorRepository.GetAll(), 
            "Id", "Name", selectedInstructorId
        );
    }
}
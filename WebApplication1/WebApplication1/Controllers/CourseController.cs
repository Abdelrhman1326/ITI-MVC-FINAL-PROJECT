using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class CourseController : Controller
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    // GET: /Courses
    public IActionResult Index()
    {
        IEnumerable<Course> allCourses = _courseRepository.GetCourses();
        return View(allCourses);
    }
    
    // GET: /Courses/Details
    public IActionResult Details(int id)
    {
        Course course = _courseRepository.GetCourse(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // GET: /Students/Create
    // Get endpoint to return the creation form
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Course course)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _courseRepository.CreateCourse(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index)); // return to the index of the page -> Courses
            }

            return View(course); // return the view if input errors found
        }
        catch (Exception exp)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the course. Please check your data.");
            return View(course);
        }
    }
    
    // GET: /Students/Edit/5
    public IActionResult Edit(int id)
    {
        var course = _courseRepository.GetCourse(id);
        if (course == null)
        {
            return NotFound();
        }
        return  View(course);
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
                _courseRepository.EditCourse(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_courseRepository.GetCourse(course.Id) == null)
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
        
        return View(course);
    }
    
    // GET: /Courses/Delete/5
    public IActionResult Delete(int id)
    {
        var course = _courseRepository.GetCourse(id);
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
        var course = _courseRepository.GetCourse(id);
        if (course == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        _courseRepository.DeleteCourse(id);
        _courseRepository.Save();
        TempData["StatusMessage"] = $"Course with ID {id} has been successfully deleted.";
        return RedirectToAction(nameof(Index));
    }
}
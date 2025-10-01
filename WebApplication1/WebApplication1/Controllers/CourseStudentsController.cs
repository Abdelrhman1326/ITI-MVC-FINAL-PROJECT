using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class CourseStudentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
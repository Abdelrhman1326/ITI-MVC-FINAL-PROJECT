using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class CourseController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
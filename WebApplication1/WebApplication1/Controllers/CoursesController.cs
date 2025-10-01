using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class CoursesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
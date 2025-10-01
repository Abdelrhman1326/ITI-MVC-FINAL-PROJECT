using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class DepartmentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
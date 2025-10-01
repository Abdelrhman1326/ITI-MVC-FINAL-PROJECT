using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class InstructorsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
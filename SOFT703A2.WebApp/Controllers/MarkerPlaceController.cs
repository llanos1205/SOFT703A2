using Microsoft.AspNetCore.Mvc;

namespace SOFT703A2.WebApp.Controllers;

public class MarkerPlaceController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
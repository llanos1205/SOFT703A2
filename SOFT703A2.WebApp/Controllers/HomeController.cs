using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.ViewModels.Shared;


namespace SOFT703A2.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "MarketPlace");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
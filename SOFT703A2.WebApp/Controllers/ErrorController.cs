﻿using Microsoft.AspNetCore.Mvc;

namespace SOFT703A2.WebApp.Controllers;

public class ErrorController : Controller
{
    [Route("Error/401")]
    public IActionResult Error401()
    {
        // Handle 401 Unauthorized error
        return View("E401");
    }

    [Route("Error/403")]
    public IActionResult Error403()
    {
        // Handle 403 Forbidden error
        return View("E403");
    }

    [Route("Error/500")]
    public IActionResult Error500()
    {
        // Handle 500 Internal Server Error
        return View("E500");
    }
}
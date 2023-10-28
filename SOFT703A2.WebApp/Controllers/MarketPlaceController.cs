using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;

namespace SOFT703A2.WebApp.Controllers;

public class MarketPlaceController : Controller
{
    private readonly IMarketPlaceViewModel _marketPlaceViewModel;

    public MarketPlaceController(IMarketPlaceViewModel vm)
    {
        _marketPlaceViewModel = vm;
    }

    public async Task<IActionResult> Index()
    {
        await _marketPlaceViewModel.GetAllAsync();
        return View(_marketPlaceViewModel);
    }
    [HttpGet]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToTrolley(string productId)
    {
        await _marketPlaceViewModel.AddToTrolley(productId);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
        return Content(trolleyJson, "application/json");
    }

    [HttpGet]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(string id)
    {
        await _marketPlaceViewModel.RemoveFromTrolley(id);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
        return Content(trolleyJson, "application/json");
    }

    [HttpGet]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(string trolleyid)
    {
        await _marketPlaceViewModel.CheckOut(trolleyid);
        await _marketPlaceViewModel.GetTrolley();
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
        return Content(trolleyJson, "application/json");
    }

    [HttpGet]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Trolley()
    {
        await _marketPlaceViewModel.GetTrolley();
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        string trolleyJson = JsonSerializer.Serialize(_marketPlaceViewModel.CurrentTrolley, options);
        return Content(trolleyJson, "application/json");
    }
}
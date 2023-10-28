using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Trolley;

namespace SOFT703A2.WebApp.Controllers;

public class TrolleyController : Controller
{
    // GET
    private readonly ITrolleyViewModel _trolleyViewModel;

    public TrolleyController(ITrolleyViewModel trolleyViewModel)
    {
        _trolleyViewModel = trolleyViewModel;
    }

    public async Task<IActionResult> Detail(string id)
    {
        await _trolleyViewModel.GetByIdAsync(id);
        return View(_trolleyViewModel);
    }
}
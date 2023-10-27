using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.User;

namespace SOFT703A2.WebApp.Controllers;

public class UserController : Controller
{
    private readonly IListUserViewModel _listUserViewModel;
    private readonly IDetailUserViewModel _detailUserViewModel;
    private readonly ICreateUserViewModel _createUserViewModel;
    public UserController(IListUserViewModel listUserViewModel, IDetailUserViewModel detailUserViewModel, ICreateUserViewModel createUserViewModel)
    {
        _listUserViewModel = listUserViewModel;
        _detailUserViewModel = detailUserViewModel;
        _createUserViewModel = createUserViewModel;
    }
    public async Task<IActionResult> List()
    {
        await _listUserViewModel.GetAllAsync();
        return View(_listUserViewModel);
    }
    public IActionResult Detail(string id)
    {
        return View(_detailUserViewModel);
    }
    public IActionResult Update(string id , DetailUserViewModel vm)
    {
        return RedirectToAction("Detail");
    }
    public IActionResult Add()
    {
        return View(_createUserViewModel);
    }
    public IActionResult Delete()
    {
        return RedirectToAction("List");
    }
    
    
}
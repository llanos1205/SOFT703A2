using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.User;

namespace SOFT703A2.WebApp.Controllers;

public class UserController : Controller
{
    private readonly IListUserViewModel _listUserViewModel;
    private readonly IDetailUserViewModel _detailUserViewModel;
    private readonly ICreateUserViewModel _createUserViewModel;

    public UserController(IListUserViewModel listUserViewModel, IDetailUserViewModel detailUserViewModel,
        ICreateUserViewModel createUserViewModel)
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

    public async Task<IActionResult> Detail(string id)
    {
        await _detailUserViewModel.Find(id);
        await _detailUserViewModel.LoadRoles();
        return View(_detailUserViewModel);
    }

    public async Task<IActionResult> Update(string id, DetailUserViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _detailUserViewModel.FirstName = vm.FirstName;
            _detailUserViewModel.LastName = vm.LastName;
            _detailUserViewModel.PhoneNumber = vm.PhoneNumber;
            _detailUserViewModel.Email = vm.Email;
            _detailUserViewModel.Id = vm.Id;
            _detailUserViewModel.SelectedRole = vm.SelectedRole;
            var result = await _detailUserViewModel.Update();
            if (result)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
        }

        return RedirectToAction("Detail");
    }

    public async Task<IActionResult> Add()
    {
        await _createUserViewModel.LoadRoles();
        return View(_createUserViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CreateUserViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _createUserViewModel.FirstName = vm.FirstName;
            _createUserViewModel.LastName = vm.LastName;
            _createUserViewModel.PhoneNumber = vm.PhoneNumber;
            _createUserViewModel.Email = vm.Email;
            await _createUserViewModel.Create();
            return RedirectToAction("List");
        }

        return View(_createUserViewModel);
    }


    public async Task<IActionResult> Delete(string id)
    {
        var result = await _createUserViewModel.Delete(id);
        if (result)
        {
            return RedirectToAction("List");
        }
        else
        {
            ModelState.AddModelError("", "Something went wrong");
        }

        return RedirectToAction("List");
    }

    [HttpGet]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterUsers(string userName, bool visitsCheckbox, bool emailCheckbox,
        bool phoneCheckbox)
    {
        try
        {
            await _listUserViewModel.UpdateUsersList(userName, visitsCheckbox, emailCheckbox, phoneCheckbox);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string filteredProductsJson = JsonSerializer.Serialize(_listUserViewModel.Users, options);

            return Content(filteredProductsJson, "application/json");
        }
        catch (Exception ex)
        {
            return BadRequest("An error occurred while filtering products.");
        }
    }
}
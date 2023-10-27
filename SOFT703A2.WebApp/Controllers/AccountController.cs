using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;
using SOFT703A2.Infrastructure.ViewModels.Auth;

namespace SOFT703A2.WebApp.Controllers;

public class AccountController : Controller
{
   private readonly ILoginViewModel _loginViewModel;
   private readonly IRegisterViewModel _registerViewModel;
   public AccountController(ILoginViewModel loginViewModel, IRegisterViewModel registerViewModel)
   {
      _loginViewModel = loginViewModel;
      _registerViewModel = registerViewModel;
   }
   public IActionResult Login()
   {
      return View(_loginViewModel);
   }

   [HttpPost]
   [AllowAnonymous]
   public async Task<IActionResult> Login(LoginViewModel viewModel)
   {
      if(ModelState.IsValid)
      {
         _loginViewModel.Email = viewModel.Email;
         _loginViewModel.Password = viewModel.Password;
         var result =  await _loginViewModel.Login();
         if (result)
         {
            return RedirectToAction("Index", "Home");
         }
         else
         {
            ModelState.AddModelError("","Login failed. Please try again.");
         }
         
      }
      return View(_loginViewModel);
   }

   public IActionResult Register()
   {
      return View(_registerViewModel);
   }
   [HttpPost]
   [AllowAnonymous]
   public async Task<IActionResult> Register(RegisterViewModel viewModel)
   {
      if (ModelState.IsValid)
      {
         if (viewModel.VerifyPasswordMatch())
         {
            _registerViewModel.FirstName = viewModel.FirstName;
            _registerViewModel.LastName = viewModel.LastName;
            _registerViewModel.PhoneNumber = viewModel.PhoneNumber;
            _registerViewModel.Email = viewModel.Email;
            _registerViewModel.Password = viewModel.Password;
            var result = await _registerViewModel.Register();
            if (result)
            {
               return RedirectToAction("Index", "Home");
            }
            else
            {
               ModelState.AddModelError("", "Registration failed. Please try again.");
            }
         }
         else
         {
            ModelState.AddModelError("", "Passwords do not match.");
         }
         
      }
      return View(viewModel);
   }

   public IActionResult LogOut()
   {
      _loginViewModel.LogOut();
      return RedirectToAction("Index", "Home");
   }
  
}
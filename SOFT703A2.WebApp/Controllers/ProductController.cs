using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;
using SOFT703A2.Infrastructure.ViewModels.Product;

namespace SOFT703A2.WebApp.Controllers;

public class ProductController : Controller
{
    private readonly IListProductViewModel _listProductViewModel;
    private readonly ICreateProductViewModel _createProductViewModel;
    private readonly IDetailProductViewModel _detailProductViewModel;

    public ProductController(IListProductViewModel listProductViewModel, ICreateProductViewModel createProductViewModel,
        IDetailProductViewModel detailProductViewModel)
    {
        _listProductViewModel = listProductViewModel;
        _createProductViewModel = createProductViewModel;
        _detailProductViewModel = detailProductViewModel;
    }

    public async Task<IActionResult> List()
    {
        await _listProductViewModel.GetAll();
        return View(_listProductViewModel);
    }

    public async Task<IActionResult> Detail(string id)
    {
        await _detailProductViewModel.Find(id);
        return View(_detailProductViewModel);
    }
    public async Task<IActionResult> Update(string id,DetailProductViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _detailProductViewModel.Name = vm.Name;
            _detailProductViewModel.Photo = vm.Photo;
            _detailProductViewModel.Price = vm.Price;
            _detailProductViewModel.Stock = vm.Stock;
            var result = await _detailProductViewModel.Update(id);
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

    public IActionResult Add()
    {
        return View(_createProductViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CreateProductViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _createProductViewModel.Name = vm.Name;
            _createProductViewModel.Photo = vm.Photo;
            _createProductViewModel.Price = vm.Price;
            _createProductViewModel.Stock = vm.Stock;
            var result = await _createProductViewModel.Create();
            if (result)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
        }

        return View(vm);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _createProductViewModel.Delete(id);
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
}
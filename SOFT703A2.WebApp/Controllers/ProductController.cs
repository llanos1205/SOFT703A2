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

    [Authorize]
    public async Task<IActionResult> List()
    {
        await _listProductViewModel.GetAll();
        return View(_listProductViewModel);
    }

    public async Task<IActionResult> Detail(string id)
    {
        await _detailProductViewModel.Find(id);
        await _detailProductViewModel.LoadCategories();
        return View(_detailProductViewModel);
    }

    public async Task<IActionResult> Update(string id, DetailProductViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _detailProductViewModel.Name = vm.Name;
            _detailProductViewModel.Photo = vm.Photo;
            _detailProductViewModel.Price = vm.Price;
            _detailProductViewModel.Stock = vm.Stock;
            _detailProductViewModel.IsPromoted = vm.IsPromoted;
            _detailProductViewModel.SelectedCategory = vm.SelectedCategory;
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

    public async Task<IActionResult> Add()
    {
        await _createProductViewModel.LoadCategories();
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
            _createProductViewModel.SelectedCategory = vm.SelectedCategory;
            _createProductViewModel.IsPromoted = vm.IsPromoted;
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

    public async Task<IActionResult> Promote(string id)
    {
        await _detailProductViewModel.Promote(id);
        return RedirectToAction("List");
    }
    public async Task<IActionResult> UnPromote(string id)
    {
        await _detailProductViewModel.UnPromote(id);
        return RedirectToAction("List");
    }
}
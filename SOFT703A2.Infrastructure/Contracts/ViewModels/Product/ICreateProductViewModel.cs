using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

public interface ICreateProductViewModel
{
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public List<SelectListItem>? Categories { get; set; }
    public string SelectedCategory { get; set; }
    public Task<bool> Create();
    public Task<bool> Delete(string id);
    public Task LoadCategories();
    
    
    
}
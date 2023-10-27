using System.ComponentModel.DataAnnotations;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

public interface ICreateProductViewModel
{
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public Task<bool> Create();
    public Task<bool> Delete(string id);
    
}
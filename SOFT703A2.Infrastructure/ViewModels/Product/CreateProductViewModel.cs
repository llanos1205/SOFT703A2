using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

namespace SOFT703A2.Infrastructure.ViewModels.Product;

using SOFT703A2.Domain.Models;

public class CreateProductViewModel : ICreateProductViewModel
{
    [Required] public string? Name { get; set; }
    [Required] public string? Photo { get; set; }
    [Required] public int Stock { get; set; }
    [Required] public double Price { get; set; }

    private readonly IProductRepository _productRepository;

    public CreateProductViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public CreateProductViewModel()
    {
    }

    public async Task<bool> Create()
    {
        var result = await _productRepository.AddAsync(new Product()
        {
            Name = this.Name,
            Photo = this.Photo,
            Stock = this.Stock,
            Price = this.Price
        });
        return result != null;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _productRepository.DeleteAsync(id);
        return result != null;
    }
}
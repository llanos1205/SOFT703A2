using System.ComponentModel.DataAnnotations;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

namespace SOFT703A2.Infrastructure.ViewModels.Product;

public class DetailProductViewModel : IDetailProductViewModel
{
    [Required]
    public string? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Photo { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public double Price { get; set; }

    private readonly IProductRepository _productRepository;

    public DetailProductViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public DetailProductViewModel()
    {
    }

    public async Task<bool> Update(string id)

    {
        var product = await _productRepository.GetByIdAsync(id);
        product.Name = this.Name;
        product.Photo = this.Photo;
        product.Stock = this.Stock;
        product.Price = this.Price;
        var result = await _productRepository.UpdateAsync(product);
        return result != null;
    }

    public async Task<bool> Find(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            this.Name = product.Name;
            this.Photo = product.Photo;
            this.Stock = product.Stock;
            this.Price = product.Price;
            this.Id = product.Id;
            return true;
        }

        return false;
    }
}
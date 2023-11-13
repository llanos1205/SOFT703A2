using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Home;

namespace SOFT703A2.Infrastructure.ViewModels.Home;

using SOFT703A2.Domain.Models;

public class HomeViewModel : IHomeViewModel
{
    public List<Product>? PromotedProducts { get; set; }

    private readonly IProductRepository _productRepository;

    public HomeViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public HomeViewModel()
    {
    }

    public async Task Load()
    {
        PromotedProducts = await _productRepository.GetAllWithCategoriesAsync();
        PromotedProducts = PromotedProducts.Where(x => x.IsPromoted).ToList();
    }
}
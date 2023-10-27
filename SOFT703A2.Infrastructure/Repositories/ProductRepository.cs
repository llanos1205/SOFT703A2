
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;
public class ProductRepository: BaseRepository<Product>,IProductRepository
{
    
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
}
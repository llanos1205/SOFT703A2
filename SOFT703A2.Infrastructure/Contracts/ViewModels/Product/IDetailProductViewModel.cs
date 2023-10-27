namespace SOFT703A2.Infrastructure.Contracts.ViewModels.Product;

public interface IDetailProductViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public Task<bool> Update(string id);
    public Task<bool> Find(string id);
}
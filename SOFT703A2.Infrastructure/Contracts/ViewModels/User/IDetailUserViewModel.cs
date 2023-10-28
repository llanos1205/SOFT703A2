using SOFT703A2.Domain.Models;

namespace SOFT703A2.Infrastructure.Contracts.ViewModels.User;

public interface IDetailUserViewModel
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public List<Trolley> Trolleys { get; set; }
    public Task<bool> Update();
    public Task<bool> Find(string id);
}
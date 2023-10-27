namespace SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Domain.Models;
public interface IListUserViewModel
{
    public List<User> Users { get; set; }
    public Task GetAllAsync();
}
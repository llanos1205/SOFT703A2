using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;

namespace SOFT703A2.Infrastructure.ViewModels.User;

public class ListUserViewModel:IListUserViewModel
{
    public List<Domain.Models.User> Users { get; set; }
    private readonly IUserRepository _userRepository;
    public ListUserViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public ListUserViewModel()
    {
    }
    public async Task GetAllAsync()
    {
        Users = await _userRepository.GetAllAsync();
    }
}
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;

namespace SOFT703A2.Infrastructure.ViewModels.User;
using SOFT703A2.Domain.Models;
public class CreateUserViewModel:ICreateUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    private readonly IUserRepository _userRepository;
    public CreateUserViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public CreateUserViewModel()
    {
        
    }
    public async Task Create()
    {
        await _userRepository.AddDefaultAsync(new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            UserName = Email,
            
        },"Password123!");

    }

    public async Task<bool> Delete(string id)
    {
        var result = await _userRepository.DeleteAsync(id);
        return result != null;
    }
}
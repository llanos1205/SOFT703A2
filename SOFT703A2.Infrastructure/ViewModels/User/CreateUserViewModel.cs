using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;
using SOFT703A2.Infrastructure.ViewModels.Shared;

namespace SOFT703A2.Infrastructure.ViewModels.User;

using SOFT703A2.Domain.Models;

public class CreateUserViewModel : ICreateUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? SelectedRole { get; set; }
    public List<DropdownOption> Roles { get; set; }

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateUserViewModel(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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
        }, "Password123!");
        await _userRepository.SetRole(Email, await _roleRepository.GetRoleId(SelectedRole));
    }

    public async Task LoadRoles()
    {
        var rols = await _roleRepository.GetAllAsync();
        Roles = rols.Select(x => new DropdownOption()
        {
            Value = x.Id,
            Text = x.Name
        }).ToList();
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _userRepository.DeleteAsync(id);
        return result != null;
    }
}
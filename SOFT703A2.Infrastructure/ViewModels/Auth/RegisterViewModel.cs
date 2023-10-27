using System.ComponentModel.DataAnnotations;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;

namespace SOFT703A2.Infrastructure.ViewModels.Auth;

public class RegisterViewModel:IRegisterViewModel
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? VerifyPassword { get; set; }
    
    private readonly IUserRepository _userRepository;
    public RegisterViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public RegisterViewModel()
    {
        
    }

    public async Task<bool> Register()
    {
        var result = await _userRepository.SignIn(new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            UserName = Email,
            
        },Password);
        return result!=null;
    }

    public bool VerifyPasswordMatch()
    {
        return Password == VerifyPassword;
    }
}
using System.ComponentModel.DataAnnotations;

using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.User;

namespace SOFT703A2.Infrastructure.ViewModels.User;
using SOFT703A2.Domain.Models;
public class DetailUserViewModel:IDetailUserViewModel
{
    [Required]
    public string? Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Email { get; set; }
    public List<Trolley>? Trolleys { get; set; }
    
    private readonly IUserRepository _userRepository;
    public DetailUserViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public DetailUserViewModel()
    {
        
    }
    public async Task<bool> Update()
    {
        var user = await _userRepository.GetByIdAsync(Id);
        user.UserName = Email;
        user.Email = Email;
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.PhoneNumber = PhoneNumber;
        
        var result = await _userRepository.UpdateAsync(user);
        return result != null;
    }
    public async Task<bool> Find(string id)
    {
        var user = await _userRepository.GetUserTrolleyTransaction(id);
        if (user != null)
        {
            this.Email = user.UserName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;
            this.Id = user.Id;
            if (user.Trolleys == null)
            {
                Trolleys = new List<Trolley>();
            }
            else
            {
                Trolleys = user.Trolleys;
            }
            return true;
        }

        return false;
    }
}
namespace SOFT703A2.Infrastructure.Contracts.ViewModels.User;

public interface ICreateUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Task Create();
    Task<bool> Delete(string id);
}
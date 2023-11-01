using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
        ApplicationDbContext context) : base(context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(string Email, string Password)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, Password))
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            await _context.Login.AddAsync(
                new Login()
                {
                    SessionDate = DateTime.Now,
                    UserId = user.Id
                }
            );
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> SignIn(User user, string Password)
    {
        var result = await _userManager.CreateAsync(user, Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            var foundUser = await _userManager.FindByEmailAsync(user.Email);
            await _context.Login.AddAsync(
                new Login()
                {
                    SessionDate = DateTime.Now,
                    UserId = foundUser.Id
                }
            );
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public string? GetUserId()
    {
        return _userManager.GetUserId(_signInManager.Context.User);
    }

    public Task<User?> GetUserTrolleyTransaction(string? id)
    {
        
        return id == null
            ? _context.Users
                .Include(x => x.Trolleys.Where(t => !t.IsCurrent).OrderBy(x=>x.TransactionDate))
                .FirstOrDefaultAsync(x => x.Id == GetUserId())
            : _context.Users
                .Include(x => x.Trolleys.Where(t => !t.IsCurrent).OrderBy(x=>x.TransactionDate))
                .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SetRole(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task AddDefaultAsync(User user, string? password)
    {
        var result = await _userManager.CreateAsync(user, password);
    }
}
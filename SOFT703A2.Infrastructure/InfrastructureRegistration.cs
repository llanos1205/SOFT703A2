using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Auth;
using SOFT703A2.Infrastructure.Persistence;
using SOFT703A2.Infrastructure.Repositories;
using SOFT703A2.Infrastructure.ViewModels.Auth;

namespace SOFT703A2.Infrastructure;

public static class InfrastructureRegistration
{

    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddIdentity<User, Role>(opt =>
        {
            opt.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>();
        services.ConfigureApplicationCookie(options => { options.LoginPath = "/Account/Login"; });
        LoadRepositories(services);
        LoadViewModels(services);
        return services;
    }

    private static void LoadRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITrolleyRepository, TrolleyRepository>();
        
    }
    private static void LoadViewModels(IServiceCollection services)
    {
        services.AddScoped<IRegisterViewModel, RegisterViewModel>();
        services.AddScoped<ILoginViewModel, LoginViewModel>();
    }
}
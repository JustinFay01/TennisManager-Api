using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tennismanager.data;
using tennismanager.service.Services;

namespace tennismanager.service.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection UseTennisManagerServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TennisManagerContext>(options =>
        options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        // Injects all Mappers
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        // services.AddScoped<ICustomerService, CustomerService>();
        // services.AddScoped<ICoachService, CoachService>();
        // services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}
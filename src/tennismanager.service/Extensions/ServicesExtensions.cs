using FluentValidation;
using Microsoft.EntityFrameworkCore;
using tennismanager_api.tennismanager.api.Models.User;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.services.Services;

namespace tennismanager_api.tennismanager.services.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection UseTennisManagerServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TennisManagerContext>(options =>
        options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        
        // Injects all Validators
        services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
        // Injects all Mappers
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<ISessionService, SessionService>();
        
        return services;
    }
}
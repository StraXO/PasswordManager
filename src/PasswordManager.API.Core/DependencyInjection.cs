using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.API.Core.Identity;
using PasswordManager.API.Core.Services;
using PasswordManager.API.Core.Services.Implementation;
using PasswordManager.Persistence.Domain;

namespace PasswordManager.API.Core;

/// <summary>
///     The dependency injection class responsible for the core project.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers the core services for the application.
    ///     This includes the controllers, domain and services.
    ///     This method should be called in the <see cref="WebApplicationBuilder.Services" /> method.
    /// </summary>
    /// <param name="services">The ServiceCollection to use</param>
    public static void RegisterCore(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.RegisterDomain();
        
        // Add services
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}
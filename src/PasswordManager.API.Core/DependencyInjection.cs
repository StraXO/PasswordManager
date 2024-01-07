using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.API.Core.Services;
using PasswordManager.API.Core.Services.Implementation;
using PasswordManager.Persistence.Domain;

namespace PasswordManager.API.Core;

public static class DependencyInjection
{
    public static void RegisterCore(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.RegisterDomain();

        // Add services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}
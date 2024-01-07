using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Persistence.PostgreSql.Repositories;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.Persistence.PostgreSql;

public static class DependencyInjection
{
    public static void RegisterPostgreSql(this IServiceCollection services)
    {
        // DbContext provides database access
        services.AddScoped<AppDbContext>();

        // Repository used to access the database for specific models
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordRepository, PasswordRepository>();
    }
}
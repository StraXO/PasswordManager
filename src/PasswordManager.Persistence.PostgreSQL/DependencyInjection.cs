﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.PostgreSql.Repositories;
using PasswordManager.Persistence.PostgreSql.Security;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.Persistence.PostgreSql;

/// <summary>
///     The dependency injection class responsible for the PostgreSQL project.
///     Injects the required dependencies to use PostgreSQL for persistence.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers the PostgreSQL persistence services for the application.
    ///     This includes the database context, repositories and services.
    ///     This method should be called in the <see cref="WebApplicationBuilder.Services" /> method.
    /// </summary>
    /// <param name="services">The ServiceCollection to use</param>
    /// <param name="connectionString">The connection string of the database to connect to.</param>
    public static void RegisterPostgreSql(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<PostgresDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<PostgresDbContext>();

        // Repository used to access the database for specific models
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordRepository, PasswordRepository>();
    }
}
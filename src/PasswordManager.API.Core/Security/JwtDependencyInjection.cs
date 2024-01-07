using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.API.Core.Security.TokenServices;
using PasswordManager.Persistence.PostgreSql;
using PasswordManager.Persistence.Domain.Models;

namespace PasswordManager.API.Core.Security;

public static class JwtDependencyInjection
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthentication(configuration);

        services.AddAuthenticationIdentity();

        return services;
    }

    private static void AddAuthenticationIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AppDbContext>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var signingKey = configuration["SigningKey"] ??
                             throw new InvalidConfigurationException("JWT Signing Key is not set.");
            var issuer = configuration["Issuer"] ?? throw new InvalidConfigurationException("JWT Issuer is not set.");
            var audience = configuration["Audience"] ??
                           throw new InvalidConfigurationException("JWT Audience is not set.");

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
    }
}
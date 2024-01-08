using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PasswordManager.API.Core.Security;

public static class JwtDependencyInjection
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add HttpContextAccessor to get access to the jwt token
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        // Add authentication and authorization
        services.AddAuthentication(configuration);
        services.AddAuthorization();

        return services;
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
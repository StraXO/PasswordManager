using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PasswordManager.API.Core.Security;

/// <summary>
///     The dependency injection class for JWT authentication.
///     Responsible of the injection of the authentication and authorization services to the <see cref="IServiceCollection"/>.
/// </summary>
public static class JwtDependencyInjection
{
    /// <summary>
    ///     Add Jwt authentication to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> used to retrieve application properties from.</param>
    /// <param name="issuerConfigurationKey">The configuration key of the issuer of the JWT</param>
    /// <param name="audienceConfigurationKey">The configuration key of the audience of the JWT</param>
    /// <param name="signingKeyConfigurationKey">The configuration key of the secret JWT signing key</param>
    /// <returns>
    ///     The <see cref="IServiceCollection"/> so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration, string issuerConfigurationKey, string audienceConfigurationKey, string signingKeyConfigurationKey)
    {
        // Add HttpContextAccessor to get access to the jwt token
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        // Add authentication and authorization
        services.AddAuthentication(configuration, issuerConfigurationKey, audienceConfigurationKey, signingKeyConfigurationKey);
        services.AddAuthorization();

        return services;
    }

    private static void AddAuthentication(this IServiceCollection services, 
        IConfiguration configuration, string issuerConfigurationKey, string audienceConfigurationKey, string signingKeyConfigurationKey)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var issuerValue = configuration[issuerConfigurationKey] ??
                         throw new InvalidConfigurationException("JWT Issuer is not set.");
            var audienceValue = configuration[audienceConfigurationKey] ??
                           throw new InvalidConfigurationException("JWT Audience is not set.");
            var signingKeyValue = configuration[signingKeyConfigurationKey] ??
                             throw new InvalidConfigurationException("JWT SigningKey is not set.");

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuerValue,
                ValidAudience = audienceValue,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKeyValue)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
    }
}
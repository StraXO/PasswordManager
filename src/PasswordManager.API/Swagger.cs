using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PasswordManager.API;

/// <summary>
///     Class responsible for adding the Swagger docs.
/// </summary>
public static class Swagger
{
    /// <summary>
    ///     Add Swagger to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add Swagger to.</param>
    /// <returns>
    ///     The <see cref="IServiceCollection"/> so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Password Manager", Version = "v1" });

            // Add JWT Authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Description = "Enter your token, the 'Bearer ' part is automatically added",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            // Make sure swagger UI requires a Bearer token for all endpoints with the [Authorize] attribute
            options.OperationFilter<SecurityRequirementOperationsFilter>();
        });

        return services;
    }

    /// <summary>
    ///     This method is used to add the lock icon to endpoints which require authentication
    /// </summary>
    /// <returns>Returns the <see cref="OpenApiSecurityRequirement"/></returns>
    private static OpenApiSecurityRequirement GetSecurityRequirement()
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        };
    }

    /// <summary>
    ///     This class is used to add the lock icon to endpoints which require authentication
    ///     and to ensure that the token is required for all endpoints with the [Authorize] attribute.
    /// </summary>
    private class SecurityRequirementOperationsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add lock icon to endpoints which require authentication - the [Authorize] attribute
            var authRequired = context.ApiDescription.CustomAttributes()
                .Any(attr => attr.GetType() == typeof(AuthorizeAttribute));

            if (authRequired)
            {
                operation.Security = new List<OpenApiSecurityRequirement> 
                {
                    GetSecurityRequirement()
                };
            }
            
            // Ensure there is no lock icon on endpoints which do not require authentication - the [AllowAnonymous] attribute
            // var noAuthRequired = context.ApiDescription.CustomAttributes()
            //     .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
            //
            // if (noAuthRequired) return;
        }
    }
}
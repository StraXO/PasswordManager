using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PasswordManager;

public static class Swagger
{
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
                Description = "Enter 'Bearer' [space] and your token",
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            // Make sure swagger UI requires a Bearer token specified
            options.OperationFilter<SecurityRequirementOperationsFilter>();
        });

        return services;
    }

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
                new List<string>()
            }
        };
    }

    /// <summary>
    ///     This class is used to add the lock icon to endpoints which require authentication
    ///     and to make sure that the JWT token is required for all endpoints with the AllowAnonymous attribute.
    /// </summary>
    private class SecurityRequirementOperationsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var noAuthRequired = context.ApiDescription.CustomAttributes()
                .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));

            if (noAuthRequired) return;

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                GetSecurityRequirement()
            };
        }
    }
}
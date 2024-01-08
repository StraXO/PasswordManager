using PasswordManager.API;
using PasswordManager.API.Core;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.PostgreSql;

namespace PasswordManager;

public static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Endpoints should be lowercase
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        // Add persistence (PostgreSQL)
        var connectionString = builder.Configuration.GetConnectionString(ConfigurationKeys.ConnectionString);
        builder.Services.RegisterPostgreSql(connectionString);

        // Add core
        builder.Services.RegisterCore();
        
        // Enable JWT Authentication
        builder.Services.AddJwtAuthentication(builder.Configuration,
            ConfigurationKeys.Jwt.Issuer,
            ConfigurationKeys.Jwt.Audience,
            ConfigurationKeys.Jwt.SigningKey);

        // Add Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStatusCodePages();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
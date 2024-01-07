using Microsoft.EntityFrameworkCore;
using PasswordManager;
using PasswordManager.API.Core;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.PostgreSql;

namespace PasswordManager;

public static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add services
        builder.Services.RegisterCore();
        builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("Jwt"));

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
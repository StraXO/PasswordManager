using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Persistence;
using PasswordManager.Persistence.Domain.Models.Records;
using PasswordManager.Persistence.Domain.Models.Requests;

namespace PasswordManager.API.Core.Services.Implementation;

public class UserService(AppDbContext context) : Service<User>(context), IUserService
{
    public Task<string> Authenticate(UserAuthenticationRequest request)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public async Task<User> AddUserAsync(UserAuthenticationRequest request)
    {
        // Create user
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        User user = new()
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password, salt: salt),
            Salt = salt
        };
        
        // Add user to database
        await context.Users.AddAsync(user).ConfigureAwait(false);
        
        // Save changes
        await context.SaveChangesAsync();
 
        return user;
    }

    public Task<bool> AnyByEmailAsync(string email)
    {
        return context.Users.AnyAsync(user => user.Email == email);
    }
}
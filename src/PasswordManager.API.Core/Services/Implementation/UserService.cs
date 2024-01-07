using System;
using System.Threading.Tasks;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Domain.Models.Requests;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services.Implementation;

public class UserService(IUserRepository repository) : IUserService
{
    public Task<string> Authenticate(AuthRequest request)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public async Task<User> AddUserAsync(AuthRequest request)
    {
        // Create user
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        User user = new()
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password, salt: salt),
            Salt = salt
        };

        return await repository.AddAsync(user);
    }

    public async Task<bool> AnyByEmailAsync(string email)
    {
        return await repository.ExistsAsync(user => user.Email == email);
    }
}
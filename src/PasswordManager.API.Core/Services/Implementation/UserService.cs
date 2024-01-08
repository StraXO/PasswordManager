using System.Threading.Tasks;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Domain.Models.Requests;
using PasswordManager.Persistence.Domain.Models.Response;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services.Implementation;

/// <summary>
///     Implementation of <see cref="IUserService"/>
/// </summary>
public class UserService(IUserRepository repository, IJwtTokenService jwtTokenService) : IUserService
{
    public async Task<AuthResponse?> Authenticate(AuthRequest request)
    {
        // Find user
        var user = await repository.FindAsync(u => u.Email == request.Email);

        if (user == null)
            return null;

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return null;

        return CreateToken(user);
    }

    public AuthResponse CreateToken(User user)
    {
        var token = jwtTokenService.CreateToken(user);

        return new AuthResponse
        {
            Token = token,
        };
    }

    public async Task<User> AddUserAsync(AuthRequest request)
    {
        User user = new()
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        return await repository.AddAsync(user);
    }

    public async Task<bool> AnyByEmailAsync(string email)
    {
        return await repository.ExistsAsync(user => user.Email == email);
    }
}
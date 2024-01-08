using System.Threading.Tasks;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Domain.Models.Requests;
using PasswordManager.Persistence.Domain.Models.Response;

namespace PasswordManager.API.Core.Services;

public interface IUserService
{
    /// <summary>
    ///     Authenticate a <see cref="User"/>.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous authentication operation.
    ///     Returns a <see cref="string"/> containing the JWT token.
    /// </returns>
    Task<AuthResponse?> Authenticate(AuthRequest request);

    AuthResponse CreateToken(User user);

    /// <summary>
    ///     Add a <see cref="User"/>.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    /// </returns>
    Task<User> AddUserAsync(AuthRequest request);

    /// <summary>
    ///     Check if a <see cref="User"/> exists by email.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous check operation.
    /// </returns>
    Task<bool> AnyByEmailAsync(string email);
}
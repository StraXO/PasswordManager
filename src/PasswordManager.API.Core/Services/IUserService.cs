using System.Threading.Tasks;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Domain.Models.Requests;
using PasswordManager.Persistence.Domain.Models.Response;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services;

/// <summary>
///     Service responsible of processing logic for the <see cref="IUserRepository"/>
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Authenticate a <see cref="User"/>.
    /// </summary>
    /// <param name="request">The authentication request.</param>
    /// <returns>
    ///     A task that represents the asynchronous authentication operation.
    ///     Returns a <see cref="AuthResponse"/> that contains the authentication token, or null if authentication failed.
    /// </returns>
    Task<AuthResponse?> Authenticate(AuthRequest request);

    /// <summary>
    ///     Create a Jwt <see cref="AuthResponse"/>.
    /// </summary>
    /// <param name="user">The user to create the token for.</param>
    /// <returns>
    ///     Returns a <see cref="AuthResponse"/> that contains the token.
    ///     The token can be used to authenticate the user.
    /// </returns>
    AuthResponse CreateToken(User user);

    /// <summary>
    ///     Add a <see cref="User"/>.
    /// </summary>
    /// <param name="request">The user to add.</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    ///     Returns the added <see cref="User"/>.
    /// </returns>
    Task<User> AddUserAsync(AuthRequest request);

    /// <summary>
    ///     Check if a <see cref="User"/> exists by email.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns>
    ///     A task that represents the asynchronous check operation.
    ///     Returns a boolean that indicates whether the email exists in the database.
    /// </returns>
    Task<bool> AnyByEmailAsync(string email);
}
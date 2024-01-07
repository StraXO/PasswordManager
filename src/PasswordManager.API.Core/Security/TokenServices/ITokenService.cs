using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.API.Core.Security.TokenServices;

/// <summary>
///     A service that handles user authentication.
/// </summary>
/// <typeparam name="T">The type of the user</typeparam>
public interface ITokenService
{
    /// <summary>
    ///     Create a new token.
    /// </summary>
    /// <param name="user">The user to create the token for</param>
    /// <returns>
    ///     Returns the token <see cref="string"/>.
    /// </returns>
    public string CreateToken(User user);
}
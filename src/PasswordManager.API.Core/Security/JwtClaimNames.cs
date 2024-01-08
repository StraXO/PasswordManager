using Microsoft.IdentityModel.JsonWebTokens;

namespace PasswordManager.API.Core.Security;

/// <summary>
///     Contains the names of custom claims used in the JWT.
///     These claims are also used to identify the user.
/// </summary>
public struct JwtClaimNames
{
    // Registered claims (https://datatracker.ietf.org/doc/html/rfc7519#section-4)

    /// <summary>
    ///     Sub - subject. Contains the user's email.
    /// </summary>
    public const string Sub = JwtRegisteredClaimNames.Sub;
    /// <summary>
    ///     Iat - Issued at. Contains the Date and Time of when the token has been created in seconds.
    /// </summary>
    public const string Iat = JwtRegisteredClaimNames.Iat;
    /// <summary>
    ///     Jti - JWT ID. The unique identifier of the token.
    ///     Used to ensure that when using multiple issuers, collisions will be prevented.
    /// </summary>
    public const string Jti = JwtRegisteredClaimNames.Jti;

    // Custom claims

    /// <summary>
    ///     UserId contains the id of the user.
    /// </summary>
    public const string UserId = "userid";
    
    /// <summary>
    ///     Role contains the role of the user.
    /// </summary>
    public const string Role = "role";
}
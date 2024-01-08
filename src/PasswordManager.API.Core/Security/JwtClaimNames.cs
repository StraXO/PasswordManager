using Microsoft.IdentityModel.JsonWebTokens;

namespace PasswordManager.API.Core.Security;

/// <summary>
///     Contains the names of custom claims used in the JWT.
///     These claims are used to identify the user and their role.
/// </summary>
public struct JwtClaimNames
{
    /// <summary>
    ///     The subject claim is set to the user's email.
    /// </summary>
    public const string Sub = JwtRegisteredClaimNames.Sub;
    public const string Iat = JwtRegisteredClaimNames.Iat;
    public const string Jti = JwtRegisteredClaimNames.Jti;

    // Custom claims
    public const string Userid = "userid";
    public const string Role = "role";
}
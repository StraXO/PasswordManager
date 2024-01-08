using System.Collections.Generic;
using System.Security.Claims;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models;

namespace PasswordManager.API.Core.Identity;

/// <summary>
///     The claim service that handles access to claims in the authentication token.
///     See <see cref="JwtClaimNames"/> for the list of claims available
/// </summary>
public interface IClaimService
{
    /// <summary>
    ///     Get all claims
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}"/> collection of <see cref="Claim"/>.
    /// </returns>
    public IEnumerable<Claim> GetClaims();
    
    /// <summary>
    ///     Get a claim.
    /// </summary>
    /// <returns>
    ///     A <see cref="string"/> value of the <see cref="Claim"/>.
    ///     It is preferred to use the specific Get{Claim}Claim method since it returns the correct type.
    /// </returns>
    public string GetClaimValue(string claimName);

    /// <summary>
    ///     Check if a <see cref="Claim"/> with the specified value exists in the list of claims.
    /// </summary>
    /// <param name="claimName">The name of the claim.</param>
    /// <param name="claimValue">The expected claim value.</param>
    /// <returns>
    ///     A <see cref="bool"/> indicating whether the claim with the given value exists.
    /// </returns>
    public bool HasClaim(string claimName, string claimValue);
    
    /// <summary>
    ///     Get the <see cref="JwtClaimNames.Sub"/> claim value.
    /// </summary>
    /// <returns>
    ///     A <see cref="string"/> with the value of the Sub claim.
    /// </returns>
    public string GetSub();
    
    /// <summary>
    ///     Get the <see cref="JwtClaimNames.UserId"/> claim value.
    /// </summary>
    /// <returns>
    ///     A <see cref="long"/> with the value of the UserId claim.
    /// </returns>
    public long GetUserId();
    
    /// <summary>
    ///     Get the <see cref="JwtClaimNames.Role"/> claim value.
    /// </summary>
    /// <returns>
    ///     A <see cref="Role"/> with the value of the Role claim.
    /// </returns>
    public Role GetRole();
    
    /// <summary>
    ///     Check if the <see cref="JwtClaimNames.Role"/> claim contains the <see cref="Role.Admin"/> role
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> indicating whether the <see cref="JwtClaimNames.Role"/> claim contains the <see cref="Role.Admin"/> role
    /// </returns>
    public bool IsAdmin();
    
    /// <summary>
    ///     Check if the <see cref="JwtClaimNames.Role"/> claim contains the <see cref="Role.User"/> role
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> indicating whether the <see cref="JwtClaimNames.Role"/> claim contains the <see cref="Role.User"/> role
    /// </returns>
    public bool IsUser();
}
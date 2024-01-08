using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PasswordManager.API.Core.Identity.Attributes;

/// <summary>
///     An <see cref="Attribute"/> that checks whether a claim contains the specified value.
///     If the claim does not have the specified value, a <see cref="ForbidResult"/> response will be returned.
/// </summary>
/// <param name="claimName">The name of the claim</param>
/// <param name="claimValue">The allowed value of the claim</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresClaimAttribute(string claimName, string claimValue) : Attribute, IAuthorizationRequirement
{
    /// <summary>
    ///     Checks if the user has the required claim.
    ///     If not, the request will be forbidden.
    ///     If the claimValue is null, the claim will only be checked for existence with the default value of true.
    /// </summary>
    /// <param name="context">The authorization context</param>
    /// <example>
    ///     For controllers that only allow users with the admin role:
    ///
    ///     [Authorize]
    ///     [RequiresClaim(claimName: "role", claimValue: Role.Admin.ToString())]
    /// </example>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.HasClaim(claimName, claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PasswordManager.API.Core.Identity;

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
    ///     [Authorize]
    ///     [RequiresClaim(claimName: "role", claimValue: "Admin")]
    /// </example>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.HasClaim(claimName, claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}
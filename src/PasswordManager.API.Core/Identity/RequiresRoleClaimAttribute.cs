using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models;

namespace PasswordManager.API.Core.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresRoleClaimAttribute(Role claimValue) : Attribute, IAuthorizationRequirement
{
    /// <summary>
    ///     Checks if the user has the required claim value for the <see cref="JwtClaimNames.Role" /> claim.
    ///     If not, the request will be forbidden.
    /// </summary>
    /// <param name="context">The authorization context</param>
    /// <example>
    ///     [Authorize]
    ///     [RequiresRoleClaim(Role.Admin)]
    /// </example>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.HasClaim(JwtClaimNames.Role, claimValue.ToString()))
        {
            context.Result = new ForbidResult();
        }
    }
}
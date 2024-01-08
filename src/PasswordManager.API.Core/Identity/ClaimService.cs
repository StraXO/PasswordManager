using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models;

namespace PasswordManager.API.Core.Identity;

public class ClaimService(IHttpContextAccessor contextAccessor) : IClaimService
{
    public IEnumerable<Claim> GetClaims()
    {
        return contextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
    }
    
    public string GetClaimValue(string claimName)
    {
        return GetClaims().FirstOrDefault(c => c.Type == claimName)?.Value ?? string.Empty;
    }
    
    public bool HasClaim(string claimName, string claimValue)
    {
        return GetClaims().Any(c => c.Type == claimName && c.Value == claimValue);
    }
    
    public string GetUserEmail()
    {
        return GetClaimValue(JwtClaimNames.Sub);
    }
    
    public long GetUserId()
    {
        return long.Parse(GetClaimValue(JwtClaimNames.Userid));
    }
    
    public Role GetRole()
    {
        return Enum.Parse<Role>(GetClaimValue(JwtClaimNames.Role));
    }
    
    public bool IsAdmin()
    {
        return HasClaim(JwtClaimNames.Role, Role.Admin.ToString());
    }
    
    public bool IsUser()
    {
        return HasClaim(JwtClaimNames.Role, Role.User.ToString());
    }
}
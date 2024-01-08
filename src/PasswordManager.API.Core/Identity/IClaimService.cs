using System.Collections.Generic;
using System.Security.Claims;
using PasswordManager.Persistence.Domain.Models;

namespace PasswordManager.API.Core.Identity;

public interface IClaimService
{
    public IEnumerable<Claim> GetClaims();
    public string GetClaimValue(string claimName);
    public bool HasClaim(string claimName, string claimValue);
    public string GetUserEmail();
    public long GetUserId();
    public Role GetRole();
    public bool IsAdmin();
    public bool IsUser();
}
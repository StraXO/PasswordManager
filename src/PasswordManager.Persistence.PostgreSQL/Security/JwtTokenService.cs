using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.API.Core.Security;
using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.Persistence.PostgreSql.Security;

/// <summary>
///     The service responsible for creating a token.
/// </summary>
/// <param name="configuration">The <see cref="IConfiguration"/> that contains a set of key/value application configuration properties.</param>
public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public string CreateToken(User user)
    {
        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials()
        );
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials) =>
        new(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            signingCredentials: credentials
        );

    private IEnumerable<Claim> CreateClaims(User user)
    {
        return
        [
            new Claim(JwtClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim(JwtClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtClaimNames.Sub, user.Email),
            new Claim( JwtClaimNames.UserId, user.Id.ToString()),
            new Claim( JwtClaimNames.Role, user.Role.ToString())
        ];
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var signingKey = configuration["Jwt:SigningKey"] ??
                         throw new InvalidConfigurationException("Jwt:SigningKey is not set.");

        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(signingKey)
            ),
            SecurityAlgorithms.HmacSha512
        );
    }
}
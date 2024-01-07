﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.API.Core.Security.TokenServices;

public class TokenService(ILogger<TokenService> logger, IConfiguration configuration) : ITokenService
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
        try
        {
            return
            [
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            ];
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating claims");
            throw;
        }
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
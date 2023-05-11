﻿using Handyman_DataLibrary.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Handyman_DataLibrary.DataAccess.Query;
public class TokenProvider : ITokenProvider
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _configuration;

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:JWTSecretKey").Value));

    }

    public string GenerateToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, email)

        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

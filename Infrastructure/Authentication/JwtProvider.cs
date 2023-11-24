using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authorization;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public sealed class JwtProvider: IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.Name),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };
        
        var settings = _configuration.GetSection("JwtSettings");
        
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(settings["Key"])),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            settings["Issuer"],
            settings["Audience"],
            claims,
            null,
            DateTime.UtcNow.Add(
                TimeSpan.Parse(
                    _configuration.GetSection("JwtSettings:ExpiryTime").Value!
                    )
                ),
            signingCredentials
            );

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
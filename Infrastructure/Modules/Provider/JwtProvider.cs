using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Modules.Auth.Interface;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Modules.Provider;

public class JwtProvider(IConfiguration config): IJwtProvider
{
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role!) 
        };
        
        int expireMinutes = int.Parse(config["Jwt:AccessExpireMinutes"] ?? "60");

        return CreateToken(claims, expireMinutes);
    }
    
    private string CreateToken(IEnumerable<Claim> claims, int expireMinutes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes), 
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    
    public string GenerateActionToken(User user, string actionType)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("ActionType", actionType) 
        };
        
  
        int expireMinutes = int.Parse(config["Jwt:ActionExpireMinutes"] ?? "15");

        return CreateToken(claims, expireMinutes);
    }
}
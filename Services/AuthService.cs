using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VocabularyAPI.Services;

public class AuthService(IConfiguration configuration)
{
    public byte[] RetrieveJwtSecretKey()
    {
        var secretKey = configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is missing");
        return Encoding.ASCII.GetBytes(secretKey);
    }
    
    public string GenerateJwtToken(string email)
    {
        var tokenKey = RetrieveJwtSecretKey();
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "VocabularyAPI",
            Audience = "VocabularyAPI",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}
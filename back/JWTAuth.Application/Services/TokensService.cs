using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JWTAuth.Core.Enums;


namespace JWTAuth.Application.Services;

public class TokensService : ITokensService
{
    private readonly IConfiguration _configuration;
    public TokensService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(UserViewModel model)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("SecureToken").Value);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), model.Role))
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
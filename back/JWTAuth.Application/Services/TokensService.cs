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
using JWTAuth.Core.Interfaces;
using JWTAuth.Core.Entities;

namespace JWTAuth.Application.Services;

public class TokensService : ITokensService
{
    private readonly IInvalidTokensRepository _invalidTokensRepository;
    private readonly IConfiguration _configuration;
    public TokensService(
        IInvalidTokensRepository invalidTokensRepository, 
        IConfiguration configuration)
    {
        _invalidTokensRepository = invalidTokensRepository;
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
                new Claim("username", model.Username),
                new Claim(ClaimTypes.Role, model.Role.ToString()),
                new Claim("createdAt", model.CreatedAt.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public async Task InvalidateToken(string value)
    {
        Token token = new Token()
        {
            Value = value
        };
            
        await _invalidTokensRepository.Add(token);
    }

    public async Task<bool> IsValid(string tokenFromRequest)
    {
        if (await _invalidTokensRepository.Get(new Token() { Value = tokenFromRequest }) == null)
            return true;

        return false;
    }
}
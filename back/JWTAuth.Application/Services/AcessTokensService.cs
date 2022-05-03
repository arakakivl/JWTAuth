using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JWTAuth.Core.Interfaces;
using JWTAuth.Core.Entities;

namespace JWTAuth.Application.Services;

public class AcessTokensService : IAcessTokensService
{
    private readonly IInvalidTokensRepository _invalidTokensRepository;
    private readonly IConfiguration _configuration;
    public AcessTokensService(
        IInvalidTokensRepository invalidTokensRepository, 
        IConfiguration configuration)
    {
        _invalidTokensRepository = invalidTokensRepository;
        _configuration = configuration;
    }
    
    public async Task<string> GenerateAsync(UserViewModel model)
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
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return await Task.FromResult(handler.WriteToken(token));
    }

    public async Task<string> GenerateAsync(string acess)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("SecureToken").Value);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity((await GetClaimsAsync(acess)).Claims),
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return await Task.FromResult(handler.WriteToken(token));
    }

    public async Task InvalidateAsync(string value) =>
        await _invalidTokensRepository.AddAsync(new AccessToken() { Value = value });

    public async Task<bool> IsValidAsync(string tokenFromRequest) =>
        await _invalidTokensRepository.GetAsync(new AccessToken() { Value = tokenFromRequest }) == null;

    private async Task<ClaimsPrincipal> GetClaimsAsync(string acess)
    {
        var parameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecureToken").Value)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(acess, parameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) 
            throw new SecurityTokenException("Invalid Token!");

        return await Task.FromResult(principal);
    }
}

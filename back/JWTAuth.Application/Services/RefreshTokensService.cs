using System.Security.Claims;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuth.Application.Services;

public class RefreshTokensService : IRefreshTokensService
{
    private readonly IRefreshTokensRepository _repository;
    public RefreshTokensService(IRefreshTokensRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Guid> GenerateAsync()
    {
        var token = Guid.NewGuid();
        await _repository.AddAsync(new RefreshToken() { Value = token });

        return await Task.FromResult(token);
    }

    public async Task<Guid> GenerateAsync(Guid oldToken)
    {
        var newToken = Guid.NewGuid();

        await _repository.DeleteAsync(new RefreshToken() { Value = oldToken });
        await _repository.AddAsync(new RefreshToken() { Value = newToken });

        return await Task.FromResult(newToken);
    }

    public async Task<bool> IsValidAsync(Guid token) => 
        await _repository.GetAsync(new RefreshToken() { Value = token }) != null;
}

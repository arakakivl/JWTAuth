using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;

namespace JWTAuth.Application.Services;

public class RefreshTokensService : IRefreshTokensService
{
    private readonly IUnitOfWork _unitOfWork;
    public RefreshTokensService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> GenerateAsync()
    {
        var token = Guid.NewGuid();
        await _unitOfWork.RefreshRepository.AddAsync(new RefreshToken() { Value = token });

        return await Task.FromResult(token);
    }

    public async Task<Guid> GenerateAsync(Guid oldToken)
    {
        var newToken = Guid.NewGuid();

        await _unitOfWork.RefreshRepository.DeleteAsync(new RefreshToken() { Value = oldToken });
        await _unitOfWork.RefreshRepository.AddAsync(new RefreshToken() { Value = newToken });

        return await Task.FromResult(newToken);
    }

    public async Task<bool> IsValidAsync(Guid token) => 
        await _unitOfWork.RefreshRepository.GetByKeyValueAsync(new RefreshToken() { Value = token }) != null;
}

using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces;

public interface IRefreshTokensRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetAsync(RefreshToken token);
    Task DeleteAsync(RefreshToken token);
}

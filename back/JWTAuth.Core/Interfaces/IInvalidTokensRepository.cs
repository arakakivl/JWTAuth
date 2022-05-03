using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces;

public interface IInvalidTokensRepository
{
    Task AddAsync(AccessToken token);
    Task<AccessToken?> GetAsync(AccessToken token);
}

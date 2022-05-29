using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces.NonGenericRepositories;

public interface IRefreshTokensRepository : IGenericRepository<RefreshToken, RefreshToken>
{
    
}

using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces.NonGenericRepositories;

public interface IInvalidTokensRepository : IGenericRepository<AccessToken, AccessToken>
{
    
}

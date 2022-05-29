using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces.NonGenericRepositories;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class InvalidTokensRepository : GenericRepository<AccessToken, AccessToken>, IInvalidTokensRepository
{
    public InvalidTokensRepository(AppDbContext context) : base (context)
    {
        
    }
}

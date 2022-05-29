using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces.NonGenericRepositories;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class RefreshTokensRepository : GenericRepository<RefreshToken, RefreshToken>, IRefreshTokensRepository
{
    public RefreshTokensRepository(AppDbContext context) : base(context)
    {
        
    }
}

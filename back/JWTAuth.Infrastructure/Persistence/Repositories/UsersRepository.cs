using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces.NonGenericRepositories;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class UsersRepository : GenericRepository<int, User>, IUsersRepository
{
    public UsersRepository(AppDbContext context) : base(context)
    {
        
    }
}

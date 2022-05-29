using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces.NonGenericRepositories;

public interface IUsersRepository : IGenericRepository<int, User>
{

}

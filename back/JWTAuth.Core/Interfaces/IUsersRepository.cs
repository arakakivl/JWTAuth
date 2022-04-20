using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces;

public interface IUsersRepository
{
    Task Create(User u);

    Task<IEnumerable<User?>> GetAll();
    Task<User?> Get(int id);

    Task Update(User u);
    Task Delete(int id);
}
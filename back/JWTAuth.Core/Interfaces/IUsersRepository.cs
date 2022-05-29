using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces;

public interface IUsersRepository
{
    Task CreateAsync(User u);

    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetAsync(int id);

    Task UpdateAsync(User u);
    Task DeleteAsync(int id);
}

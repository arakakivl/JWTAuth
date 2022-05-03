using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;
    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(User u)
    {
        _context.Users.Add(u);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(User u)
    {
        var entity = await _context.FindAsync<User>(u.Id);
        if (entity is null)
            return;
        
        _context.Entry(entity).CurrentValues.SetValues(u);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.FindAsync<User>(id);
        if (entity is null)
            return;

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

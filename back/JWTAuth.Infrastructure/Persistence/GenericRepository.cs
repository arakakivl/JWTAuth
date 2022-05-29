using JWTAuth.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence;

public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await Task.CompletedTask;
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_dbSet.AsNoTracking().AsQueryable());
    }

    public async Task<TEntity?> GetByKeyValueAsync(TKey key)
    {
        return await _dbSet.FindAsync(key);
    }

    public async Task UpdateAsync(TKey key, TEntity updatedEntity)
    {
        var toUpdate = await _dbSet.FindAsync(key);
        if (toUpdate is null)
            return;

        _dbSet.Update(updatedEntity);
    }

    public async Task DeleteAsync(TKey key)
    {
        var toDelete = await _dbSet.FindAsync(key);
        if (toDelete is null)
            return;

        _dbSet.Remove(toDelete);
    }
}
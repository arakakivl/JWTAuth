namespace JWTAuth.Core.Interfaces;

public interface IGenericRepository<TKey, TEntity>
{
    Task AddAsync(TEntity entity);

    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByKeyValueAsync(TKey key);

    Task UpdateAsync(TKey key, TEntity updatedEntity);
    Task DeleteAsync(TKey key);
}
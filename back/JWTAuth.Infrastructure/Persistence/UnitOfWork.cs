using JWTAuth.Core.Interfaces;
using JWTAuth.Core.Interfaces.NonGenericRepositories;
using JWTAuth.Infrastructure.Persistence.Repositories;

namespace JWTAuth.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private readonly IUsersRepository _users;
    private readonly IRefreshTokensRepository _refresh;
    private readonly IInvalidTokensRepository _invalidAccess;

    private bool isDisposed;

    public IUsersRepository UsersRepository => _users;

    public IRefreshTokensRepository RefreshRepository => _refresh;

    public IInvalidTokensRepository InvalidRepository => _invalidAccess;

   public UnitOfWork(AppDbContext context)
    {
        _context = context;

        _users ??= new UsersRepository(context);
        _refresh ??= new RefreshTokensRepository(context);
        _invalidAccess ??= new InvalidTokensRepository(context);

    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposed)
            if (isDisposing)
                _context.Dispose();
        
        isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
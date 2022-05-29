using JWTAuth.Core.Interfaces.NonGenericRepositories;

namespace JWTAuth.Core.Interfaces;

public interface IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
    IRefreshTokensRepository RefreshRepository { get; }
    IInvalidTokensRepository InvalidRepository { get; }

    Task SaveChangesAsync();
}
using JWTAuth.Core.Entities;

namespace JWTAuth.Core.Interfaces;

public interface IInvalidTokensRepository
{
    Task Add(Token token);
    Task<Token?> Get(Token token);
}
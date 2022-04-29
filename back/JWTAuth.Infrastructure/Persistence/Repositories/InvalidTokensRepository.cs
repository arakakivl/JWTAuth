using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class InvalidTokensRepository : IInvalidTokensRepository
{
    private readonly AppDbContext _dbContext;
    public InvalidTokensRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task Add(Token token)
    {
        if (await _dbContext.InvalidTokens.SingleOrDefaultAsync(x => x.Value == token.Value) != null)
            await Task.CompletedTask;
        else
            _dbContext.Add(token);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Token?> Get(Token token)
    {
        var tokn = await _dbContext.InvalidTokens.SingleOrDefaultAsync(x => x.Value == token.Value);
        return tokn;

    }
}
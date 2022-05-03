using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class InvalidTokensRepository : IInvalidTokensRepository
{
    private readonly AppDbContext _context;
    public InvalidTokensRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AccessToken token)
    {
        _context.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<AccessToken?> GetAsync(AccessToken token)
    {
        var tokn = await _context.InvalidTokens.SingleOrDefaultAsync(x => x.Value == token.Value);
        return tokn;
    }
}

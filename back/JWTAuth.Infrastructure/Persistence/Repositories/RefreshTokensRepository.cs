using JWTAuth.Core.Entities;
using JWTAuth.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence.Repositories;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly AppDbContext _context;
    public RefreshTokensRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetAsync(RefreshToken token)
    {
        return await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(x => x.Value == token.Value);
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }
}

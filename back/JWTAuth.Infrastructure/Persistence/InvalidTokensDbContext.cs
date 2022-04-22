using JWTAuth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence;

public class InvalidTokensDbContext : DbContext
{
    public InvalidTokensDbContext(DbContextOptions<InvalidTokensDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<Token> InvalidTokens { get; set; } = null!;
}

using JWTAuth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<AccessToken> InvalidTokens => Set<AccessToken>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}

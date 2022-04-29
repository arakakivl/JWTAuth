using JWTAuth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Token> InvalidTokens { get; set; } = null!;
}

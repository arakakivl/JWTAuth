using JWTAuth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Infrastructure.Persistence;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<User> Users { get; set; } = null!;
}

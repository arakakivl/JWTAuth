using System.ComponentModel.DataAnnotations;
using JWTAuth.Core.Enums;

namespace JWTAuth.Core.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

using System.ComponentModel.DataAnnotations;
using JWTAuth.Core.Enums;

namespace JWTAuth.Core.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public Role Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
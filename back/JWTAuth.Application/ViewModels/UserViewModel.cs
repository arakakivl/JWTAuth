using JWTAuth.Core.Enums;

namespace JWTAuth.Application.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

using JWTAuth.Core.Enums;

namespace JWTAuth.Application.ViewModels;

public class AdmViewModel
{
    public string Username { get; set; } = null!;
    public Role Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

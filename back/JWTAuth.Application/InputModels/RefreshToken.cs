using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Application.InputModels;

public class Refresh
{
    [Required]
    public string OldAccessToken { get; set; } = null!;

    [Required]
    public Guid OldRefreshToken { get; set; }
}
namespace JWTAuth.Application.InputModels;

public class Refresh
{
    public string? OldAccessToken { get; set; }
    public Guid? OldRefreshToken { get; set; }
}
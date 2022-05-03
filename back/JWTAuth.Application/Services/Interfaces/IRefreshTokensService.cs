namespace JWTAuth.Application.Services.Interfaces;

public interface IRefreshTokensService
{
    Task<Guid> GenerateAsync();
    Task<Guid> GenerateAsync(Guid oldToken);

    Task<bool> IsValidAsync(Guid token);
}

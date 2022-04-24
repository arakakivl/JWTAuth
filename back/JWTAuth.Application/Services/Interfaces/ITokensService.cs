using JWTAuth.Application.ViewModels;

namespace JWTAuth.Application.Services.Interfaces;

public interface ITokensService
{
    string GenerateToken(UserViewModel model);
    Task InvalidateToken(string value);

    Task<bool> IsValid(string tokenFromRequest);
}
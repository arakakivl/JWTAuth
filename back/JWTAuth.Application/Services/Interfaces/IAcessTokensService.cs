using System.Security.Claims;
using JWTAuth.Application.ViewModels;

namespace JWTAuth.Application.Services.Interfaces;

public interface IAcessTokensService
{
    Task<string> GenerateAsync(UserViewModel model);
    Task<string> GenerateAsync(string acess);

    Task InvalidateAsync(string value);
    Task<bool> IsValidAsync(string tokenFromRequest);
}

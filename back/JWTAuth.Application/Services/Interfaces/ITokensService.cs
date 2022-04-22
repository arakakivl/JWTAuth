using JWTAuth.Application.InputModels;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Entities;

namespace JWTAuth.Application.Services.Interfaces;

public interface ITokensService
{
    string GenerateToken(UserViewModel model);
    Task InvalidateToken(string value);

    Task<bool> IsValid(string tokenFromRequest);
}
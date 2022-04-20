using JWTAuth.Application.InputModels;
using JWTAuth.Application.ViewModels;

namespace JWTAuth.Application.Services.Interfaces;

public interface ITokensService
{
    string GenerateToken(UserViewModel model);
}
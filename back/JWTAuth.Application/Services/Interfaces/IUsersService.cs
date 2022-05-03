using JWTAuth.Application.InputModels;
using JWTAuth.Application.ViewModels;

namespace JWTAuth.Application.Services.Interfaces;

public interface IUsersService
{ 
    Task RegisterAsync(UserRegister model);
    Task<UserViewModel?> GetAsync(string? nameOrEmail);
}
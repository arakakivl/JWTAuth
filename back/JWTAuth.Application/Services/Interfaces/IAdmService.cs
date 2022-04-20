using JWTAuth.Application.InputModels;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;

namespace JWTAuth.Application.Services.Interfaces;

public interface IAdmService
{ 
    Task<IEnumerable<AdmViewModel>> GetAll();
    Task<AdmViewModel?> GetByUsername(string? username);

    Task<bool> ChangeRole(string? username, Role role);

    Task<bool> Delete(string? username);
}
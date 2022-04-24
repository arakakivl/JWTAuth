using JWTAuth.Application.InputModels;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;

namespace JWTAuth.Application.Services.Interfaces;

public interface IAdmService
{ 
    Task<IEnumerable<AdmViewModel>> GetAll();
    Task<IEnumerable<AdmViewModel>> GetByRole(Role role);
    Task<AdmViewModel?> GetByUsername(string username);
    Task<IEnumerable<AdmViewModel>> Search(string username, Role role);

    Task<bool> ChangeRole(string username, Role role);

    Task<bool> Delete(string username);
}
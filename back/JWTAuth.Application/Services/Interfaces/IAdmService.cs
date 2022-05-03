using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;

namespace JWTAuth.Application.Services.Interfaces;

public interface IAdmService
{ 
    Task<IEnumerable<AdmViewModel>> GetAllAsync();

    Task<IEnumerable<AdmViewModel>> GetByRoleAsync(Role role);
    Task<AdmViewModel?> GetByUsernameAsync(string username);

    Task<IEnumerable<AdmViewModel>> GetByBothAsync(string username, Role role);

    Task<bool> ChangeRole(string username, Role role);
    Task<bool> Delete(string username);
}

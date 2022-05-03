using JWTAuth.Application.Extensions;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using JWTAuth.Core.Interfaces;

namespace JWTAuth.Application.Services;

public class AdmService : IAdmService
{
    private readonly IUsersRepository _repository;
    public AdmService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AdmViewModel>> GetAllAsync()
    {
        return (await _repository.GetAllAsync()).Select(x => x.AsAdmModel());
    }

    public async Task<IEnumerable<AdmViewModel>> GetByRoleAsync(Role role)
    {
        return (await _repository.GetAllAsync())
            .Select(x => x.AsAdmModel())
            .Where(x => x.Role == role);
    }

    public async Task<AdmViewModel?> GetByUsernameAsync(string username)
    {
        return (await _repository.GetAllAsync())
            .Where(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase) && x != null)
            .Select(x => x.AsAdmModel())
    
        .SingleOrDefault();
    }

    public async Task<IEnumerable<AdmViewModel>> GetByBothAsync(string username, Role role)
    {
        return (await _repository.GetAllAsync())
        .Select(x => x.AsAdmModel())
        .Where(
            x => x.Username!.ToLower().Contains(username.ToLower()) && x.Role == role);
    }

    public async Task<bool> ChangeRole(string username, Role role)
    {
        var user = (await _repository.GetAllAsync())
            .Where(x => x != null && string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase))
            .SingleOrDefault();
            
        if (user is null)
            return false;

        user.Role = role;

        await _repository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> Delete(string username)
    {
        var user = (await _repository.GetAllAsync())
        .Where(
            x => x != null && string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase))
            .SingleOrDefault();
            
        if (user is null || user.Role == Role.Admin)
            return false;

        await _repository.DeleteAsync(user.Id);
        return true;
    }
}

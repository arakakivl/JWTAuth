using JWTAuth.Application.Extensions;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using JWTAuth.Core.Interfaces;

namespace JWTAuth.Application.Services;

public class AdmService : IAdmService
{
    private readonly IUnitOfWork _unitOfWork;
    public AdmService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AdmViewModel>> GetAllAsync()
    {
        return (await _unitOfWork.UsersRepository.GetAllAsync()).Select(x => x.AsAdmModel());
    }

    public async Task<IEnumerable<AdmViewModel>> GetByRoleAsync(Role role)
    {
        return (await _unitOfWork.UsersRepository.GetAllAsync())
            .Select(x => x.AsAdmModel())
                .Where(x => x.Role == role);
    }

    public async Task<AdmViewModel?> GetByUsernameAsync(string username)
    {
        return (await _unitOfWork.UsersRepository.GetAllAsync())
            .Select(x => x.AsAdmModel())
                .SingleOrDefault(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<IEnumerable<AdmViewModel>> GetByBothAsync(string username, Role role)
    {
        return (await _unitOfWork.UsersRepository.GetAllAsync())
            .Select(x => x.AsAdmModel())
                .Where(x => x.Username.ToLower().Contains(username.ToLower()) && x.Role == role);
    }

    public async Task<bool> ChangeRole(string username, Role role)
    {
        var user = (await _unitOfWork.UsersRepository.GetAllAsync())
            .SingleOrDefault(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
            
        if (user is null)
            return false;

        user.Role = role;

        await _unitOfWork.UsersRepository.UpdateAsync(user.Id, user);
        return true;
    }

    public async Task<bool> Delete(string username)
    {
        var user = (await _unitOfWork.UsersRepository.GetAllAsync())
            .SingleOrDefault(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
            
        if (user is null || user.Role == Role.Admin)
            return false;

        await _unitOfWork.UsersRepository.DeleteAsync(user.Id);
        return true;
    }
}

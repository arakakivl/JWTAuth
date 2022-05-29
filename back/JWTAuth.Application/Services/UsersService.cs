using JWTAuth.Application.Extensions;
using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Entities;
using JWTAuth.Core.Enums;
using JWTAuth.Core.Interfaces;

namespace JWTAuth.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task RegisterAsync(UserRegister model)
    {
        Role role = (await _unitOfWork.UsersRepository.GetAllAsync()).Count() == 0 ? Role.Admin : Role.User;
        var user = new User()
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Role = role,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _unitOfWork.UsersRepository.AddAsync(user);
    }

    public async Task<UserViewModel?> GetAsync(string? nameOrEmail)
    {
        var userByName = (await _unitOfWork.UsersRepository.GetAllAsync()).SingleOrDefault(x => string.Equals(x.Username, nameOrEmail, StringComparison.InvariantCultureIgnoreCase));
        var userByEmail = (await _unitOfWork.UsersRepository.GetAllAsync()).SingleOrDefault(x => string.Equals(x.Email, nameOrEmail, StringComparison.InvariantCultureIgnoreCase));

        if (userByName != null && userByEmail != null)
            throw new Exception("Existe um usuário com o username igual ao email!");
        
        if (userByName == null && userByEmail == null)
            return null;

        if (userByName != null)
            return userByName.AsUserModel();
        else if (userByEmail != null)
            return userByEmail.AsUserModel();
        
        return null;
    }
}

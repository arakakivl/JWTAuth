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
    private readonly IUsersRepository _repository;
    public UsersService(IUsersRepository repository)
    {
        _repository = repository;
    }
    
    public async Task RegisterAsync(UserRegister model)
    {
        Role role = (await _repository.GetAll()).Count() == 0 ? Role.Admin : Role.User;
        var user = new User()
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Role = role,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _repository.Create(user);
    }

    public async Task<UserViewModel?> Get(string? nameOrEmail)
    {
        var userByName = (await _repository.GetAll()).SingleOrDefault(x => x != null && string.Equals(x.Username, nameOrEmail, StringComparison.InvariantCultureIgnoreCase));
        var userByEmail = (await _repository.GetAll()).SingleOrDefault(x => x != null && string.Equals(x.Email, nameOrEmail, StringComparison.InvariantCultureIgnoreCase));

        if (userByName != null && userByEmail != null)
            throw new Exception("Existe um usuário com o usuário igual ao email!");
        
        if (userByName == null && userByEmail == null)
            return null;

        if (userByName != null)
            return userByName.AsUserModel();
        else if (userByEmail != null)
            return userByEmail.AsUserModel();
        
        return null;
    }
}
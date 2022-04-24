using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Entities;

namespace JWTAuth.Application.Extensions;

public static class UserModel
{
    public static UserViewModel AsUserModel(this User? u)
    {
        if (u != null)
        {
            return new UserViewModel()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                Password = u.Password,
                CreatedAt = u.CreatedAt
            };
        }

        throw new Exception("User is null!");
    }

    public static AdmViewModel AsAdmModel(this User? u)
    {
        if (u != null)
        {
            return new AdmViewModel()
            {
                Username = u.Username,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            };
        }

        throw new Exception("Adm is null!");
    }
}
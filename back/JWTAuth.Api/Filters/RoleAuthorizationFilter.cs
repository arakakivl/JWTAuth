using JWTAuth.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuth.Api.Filters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class RoleAuthorizationFilter : AuthorizeAttribute
{
    public RoleAuthorizationFilter(params JWTAuth.Core.Enums.Role[] allowedRoles) 
    {
        var allowedRolesAsString = allowedRoles.Select(x => Enum.GetName(typeof(Role), x));
        Roles = string.Join(",", allowedRolesAsString);
    }
}
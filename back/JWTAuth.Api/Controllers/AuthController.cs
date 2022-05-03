using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using JWTAuth.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Api.Controllers;

[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    private readonly IAcessTokensService _acessService;
    private readonly IRefreshTokensService _refreshService;

    private readonly IUsersService _usersService;

    public AuthController(IAcessTokensService acessService, IRefreshTokensService refreshService, IUsersService usersService)
    {
        _acessService = acessService;
        _refreshService = refreshService;

        _usersService = usersService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> Register([FromBody] UserRegister model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Por favor, verifique seus dados e tente novamente.");
        
        var userInDb = await _usersService.GetAsync(model.Username);
        var emailInDb = await _usersService.GetAsync(model.Email);

        if (userInDb != null || emailInDb != null)
            return BadRequest("Usuário já existente!");

        await _usersService.RegisterAsync(model);
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] UserLogin model)
    {
        var user = await _usersService.GetAsync(model.Main);

        if (user is null)
            return BadRequest("Usuário, email ou senha incorretos.");

        if (user.Password == model.Password)
            return Ok(new { token = await _acessService.GenerateAsync(user), refresh = await _refreshService.GenerateAsync() });
        
        return BadRequest("Usuário, email ou senha incorretos.");
    }


    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> Refresh([FromBody] Refresh tokens)
    {
        if (tokens is null || tokens.OldRefreshToken is null || tokens.OldAccessToken is null)   
            return BadRequest();

        if (!(await _acessService.IsValidAsync(tokens.OldAccessToken)))
            return BadRequest();

        if (!await _refreshService.IsValidAsync((Guid)tokens.OldRefreshToken))
            return BadRequest();

        var newRefreshToken = await _refreshService.GenerateAsync((Guid)tokens.OldRefreshToken);
        var newAccessToken = await _acessService.GenerateAsync(tokens.OldAccessToken);
        
        return await Task.FromResult(Ok(new { token = newAccessToken, refresh = newRefreshToken }));
    }

    [HttpPost("logout")]
    [RoleAuthorizationFilter(Role.User, Role.Admin)]
    public async Task<ActionResult> Logout()
    {
        var httpToken = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);

        await _acessService.InvalidateAsync(httpToken);
        return NoContent();
    }

    [HttpPost]
    [RoleAuthorizationFilter(Role.User, Role.Admin)]
    public async Task<ActionResult<bool>> TokenValid()
    {
        var httpToken = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (httpToken is null)
            return BadRequest();
        
        var result = await _acessService.IsValidAsync(httpToken);
        return Ok(result);
    }

    private static string FormatHttpToken(string token)
    {
        return token.Replace("Bearer ", "");
    }
}
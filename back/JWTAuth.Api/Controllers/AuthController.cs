using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using JWTAuth.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTAuth.Core.Entities;

namespace JWTAuth.Api.Controllers;

[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    private readonly ITokensService _tokensService;
    private readonly IUsersService _usersService;

    public AuthController(ITokensService tokensService, IUsersService usersService)
    {
        _tokensService = tokensService;
        _usersService = usersService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> Register([FromBody] UserRegister model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Por favor, verifique seus dados e tente novamente.");
        
        var userInDb = await _usersService.Get(model.Username);
        var emailInDb = await _usersService.Get(model.Email);

        if (userInDb != null || emailInDb != null)
            return BadRequest("Usuário já existente!");

        await _usersService.RegisterAsync(model);

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] UserLogin model)
    {
        var user = await _usersService.Get(model.Main);

        if (user is null)
            return BadRequest("Usuário, email ou senha incorretos.");

        if (user.Password == model.Password)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(new { token = _tokensService.GenerateToken(user) });
        }
        
        return BadRequest("Usuário, email ou senha incorretos.");
    }

    [HttpPost("logout")]
    [RoleAuthorizationFilter(Role.User, Role.Admin)]
    public async Task<ActionResult> Logout()
    {
        var httpToken = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);

        await _tokensService.InvalidateToken(httpToken);
        return NoContent();
    }

    [HttpPost]
    [RoleAuthorizationFilter(Role.User, Role.Admin)]
    public async Task<ActionResult<bool>> TokenValid()
    {
        var httpToken = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (httpToken is null)
            return BadRequest();
        
        var result = await _tokensService.IsValid(httpToken);
        Console.WriteLine(result);
        return Ok(result);
    }

    private static string FormatHttpToken(string token)
    {
        return token.Replace("Bearer ", "");
    }
}
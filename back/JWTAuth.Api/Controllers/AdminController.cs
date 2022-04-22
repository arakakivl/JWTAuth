using JWTAuth.Api.Filters;
using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Api.Controllers;

[Route("/admin")]
[ApiController]
[RoleAuthorizationFilter(Role.Admin)]
public class AdminController : ControllerBase
{
    private readonly IAdmService _admService;
    private readonly ITokensService _tokensService;
    public AdminController(IAdmService admService, ITokensService tokensService)
    {
        _admService = admService;
        _tokensService = tokensService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdmViewModel>>> GetAll([FromQuery] Role role)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
        {
            return BadRequest("Não autorizado.");
        }

        if(role == Role.User)
        {
            return Ok(await _admService.GetByRole(Role.User));
        }
        else if (role == Role.Admin)
        {
            return Ok(await _admService.GetByRole(Role.Admin));
        }

        return BadRequest("Por favor, identifique a role necessária.");
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<AdmViewModel?>> Get([FromQuery] string? username)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
        {
            return BadRequest();
        }

        var user = await _admService.GetByUsername(username);
        if (user is null)
            return NotFound("Usuário inexistente.");

        return Ok(user);
    }

    [HttpPatch]
    public async Task<ActionResult> ChangeRole([FromBody] ChangeRole model)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
        {
            return BadRequest();
        }

        if (await _admService.ChangeRole(model.Username, model.Role))
            return NoContent();
        
        return NotFound("Usuário inexistente.");
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] string? username)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
        {
            return BadRequest();
        }

        if (await _admService.Delete(username))
            return NoContent();

        return NotFound("Usuário não encontrado ou administrador.");
    }

    private static string FormatHttpToken(string token)
    {
        return token.Replace("Bearer ", "");
    }

}
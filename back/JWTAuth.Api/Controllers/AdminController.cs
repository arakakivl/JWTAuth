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
    public async Task<ActionResult<IEnumerable<AdmViewModel?>>> Get([FromQuery] string? username, [FromQuery] Role? role)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
            return BadRequest();

        if (username != null && role == null)
            return Ok(new List<AdmViewModel?>() { (await _admService.GetByUsername(username)) });
        else if (username == null && role != null)
            return Ok(await _admService.GetByRole((Role)role));
        else if (username != null && role != null)
            return Ok(await _admService.Search(username, (Role)role));
        else
            return Ok(await _admService.GetAll());
    }

    [HttpPatch]
    public async Task<ActionResult> ChangeRole([FromBody] ChangeRole model)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
            return BadRequest();

        if (model.Username is null)
            return BadRequest();

        if (await _admService.ChangeRole(model.Username, model.Role))
            return NoContent();
        
        return NotFound("Usuário inexistente.");
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] string? username)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValid(token)))
            return BadRequest();

        if (username is null)
            return BadRequest();

        var user = await _admService.GetByUsername(username);
        if (user is null || user.Role == Role.Admin)
            return BadRequest();

        if (await _admService.Delete(username))
            return NoContent();
        else
            return BadRequest();
    }

    private static string FormatHttpToken(string token)
    {
        return token.Replace("Bearer ", "");
    }

}
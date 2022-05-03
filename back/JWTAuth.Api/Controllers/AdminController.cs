using JWTAuth.Api.Filters;
using JWTAuth.Application.InputModels;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.ViewModels;
using JWTAuth.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Api.Controllers;

[Route("/admin")]
[ApiController]
[RoleAuthorizationFilter(Role.Admin)]
public class AdminController : ControllerBase
{
    private readonly IAdmService _admService;
    private readonly IAcessTokensService _tokensService;
    public AdminController(IAdmService admService, IAcessTokensService tokensService)
    {
        _admService = admService;
        _tokensService = tokensService;
    }
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdmViewModel?>>> Get([FromQuery] string? username, [FromQuery] Role? role)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValidAsync(token)))
            return BadRequest();

        if (username != null && role == null)
            return Ok(new List<AdmViewModel?>() { (await _admService.GetByUsernameAsync(username)) });
        else if (username == null && role != null)
            return Ok(await _admService.GetByRoleAsync((Role)role));
        else if (username != null && role != null)
            return Ok(await _admService.GetByBothAsync(username, (Role)role));
        else
            return Ok(await _admService.GetAllAsync());
    }

    [HttpPatch]
    public async Task<ActionResult> ChangeRole([FromBody] ChangeRole model)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValidAsync(token)) || model.Username is null)
            return BadRequest();

        if (await _admService.ChangeRole(model.Username, model.Role))
            return NoContent();
        
        return NotFound("Usuário inexistente.");
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] string? username)
    {
        var token = FormatHttpToken(HttpContext.Request.Headers["Authorization"][0]);
        if (!(await _tokensService.IsValidAsync(token)) || username is null)
            return BadRequest();

        var user = await _admService.GetByUsernameAsync(username);
        if (user is null || user.Role == Role.Admin)
            return BadRequest();

        if (await _admService.Delete(username))
            return NoContent();
            
        return BadRequest();
    }

    private static string FormatHttpToken(string token)
    {
        return token.Replace("Bearer ", "");
    }

}
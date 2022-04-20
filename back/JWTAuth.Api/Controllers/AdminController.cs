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
    public AdminController(IAdmService admService)
    {
        _admService = admService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AdmViewModel>>> GetAll()
    {
        return Ok(await _admService.GetAll());
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<AdmViewModel?>> Get([FromQuery] string? username)
    {
        return Ok(await _admService.GetByUsername(username));
    }

    [HttpPatch]
    public async Task<ActionResult> ChangeRole([FromBody] ChangeRole model)
    {
        if (await _admService.ChangeRole(model.Username, model.Role))
            return NoContent();
        
        return NotFound();
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] string? username)
    {
        if (await _admService.Delete(username))
            return NoContent();

        return NotFound("Usuário não encontrado ou administrador.");
    }
}
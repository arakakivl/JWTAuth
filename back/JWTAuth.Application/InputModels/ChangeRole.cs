using System.ComponentModel.DataAnnotations;
using JWTAuth.Core.Enums;

namespace JWTAuth.Application.InputModels;

public class ChangeRole
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    public Role Role { get; set; }
}
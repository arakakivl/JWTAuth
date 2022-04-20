using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Application.InputModels;

public class UserLogin
{
    [Required]
    public string? Main { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos seis caracteres.")]
    public string? Password { get; set; }
}
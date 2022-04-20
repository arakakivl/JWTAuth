using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Application.InputModels;

public class UserRegister
{
    [Required(ErrorMessage = "Username inválido.")]
    [StringLength(20, ErrorMessage = "O comprimento máximo é de vinte caracteres.")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "Endereço de e-mail inválido.")]
    [EmailAddress(ErrorMessage = "Endereço de e-mail inválido.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos seis caracteres.")]
    public string? Password { get; set; }
}
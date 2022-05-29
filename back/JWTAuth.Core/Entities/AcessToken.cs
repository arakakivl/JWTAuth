using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Core.Entities;

public class AccessToken
{
    [Key]
    public string Value { get; set; } = null!;
}

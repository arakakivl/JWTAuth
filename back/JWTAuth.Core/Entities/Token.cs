using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Core.Entities;

public class Token
{
    [Key]
    public string Value { get; set; }
}
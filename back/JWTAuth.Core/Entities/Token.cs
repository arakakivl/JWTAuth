using System.ComponentModel.DataAnnotations;
using JWTAuth.Core.Enums;

namespace JWTAuth.Core.Entities;

public class Token
{
    [Key]
    public string Value { get; set; }
}
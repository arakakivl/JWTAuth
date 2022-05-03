using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Core.Entities;

public class RefreshToken
{

    [Key]
    public Guid Value { get; set; }
}

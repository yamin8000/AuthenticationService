using System.ComponentModel.DataAnnotations;

namespace Authentication.Application.Dtos;

public class UserUpdateDto
{
    [StringLength(255, MinimumLength = 4)]
    public string? Username { get; set; }

    [MinLength(8)]
    public string? Password { get; set; }
}
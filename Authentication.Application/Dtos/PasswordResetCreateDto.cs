using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class PasswordResetCreateDto
{
    public required string Token { get; set; }
    public required bool IsUsed { get; set; }
    public UserChannel? UserChannel { get; set; }
}
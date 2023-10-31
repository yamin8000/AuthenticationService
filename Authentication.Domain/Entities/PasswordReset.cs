using Authentication.Domain.Enums;

namespace Authentication.Domain.Entities;

public class PasswordReset : BaseEntity
{
    public required int UserId { get; set; }
    public required Channel Channel { get; set; }
    public string? Token { get; set; }
    public bool IsUsed { get; set; } = false;
}
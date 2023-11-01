using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class PasswordResetDto : BaseEntity
{
    public string? Token { get; set; }
    public bool IsUsed { get; set; }
    
    public UserChannelDto? UserChannel { get; set; }
}
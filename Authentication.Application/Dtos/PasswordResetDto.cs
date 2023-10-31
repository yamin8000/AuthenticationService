namespace Authentication.Domain.Entities;

public class PasswordResetDto : BaseEntity
{
    public string? Token { get; set; }
    public bool IsUsed { get; set; }
    
    public required int UserChannelId { get; set; }
    public UserChannelDto? UserChannel { get; set; }
}
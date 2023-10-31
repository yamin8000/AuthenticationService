namespace Authentication.Domain.Entities;

public class PasswordReset : BaseEntity
{
    public string? Token { get; set; }
    public bool IsUsed { get; set; }
    
    public required int UserChannelId { get; set; }
    public UserChannel? UserChannel { get; set; }
}
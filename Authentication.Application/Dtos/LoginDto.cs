namespace Authentication.Domain.Entities;

public class LoginDto : BaseEntity
{
    public required string Ip { get; set; }
    public bool IsSuccess { get; set; }
    
    public int UserChannelId { get; set; }
    public UserChannelDto? UserChannel { get; set; }
}
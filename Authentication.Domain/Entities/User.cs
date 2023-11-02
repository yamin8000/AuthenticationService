namespace Authentication.Domain.Entities;

public class User : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    
    public ICollection<UserChannel> Channels { get; set; } = new List<UserChannel>();
}
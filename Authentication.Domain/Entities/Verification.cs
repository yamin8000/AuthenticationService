namespace Authentication.Domain.Entities;

public class Verification : BaseEntity
{
    public required string Code { get; set; }

    public required Guid UserChannelId { get; set; }

    public User? User { get; set; }
}
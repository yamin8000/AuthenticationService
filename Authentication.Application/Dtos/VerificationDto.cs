namespace Authentication.Domain.Entities;

public class VerificationDto : BaseEntity
{
    public required string Code { get; set; }

    public required int UserChannelId { get; set; }
    public UserChannelDto? UserChannel { get; set; }
}
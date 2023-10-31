using Authentication.Domain.Enums;

namespace Authentication.Domain.Entities;

public class Verification : BaseEntity
{
    public required string Code { get; set; }

    public required int UserChannelId { get; set; }
    public UserChannel? UserChannel { get; set; }
}
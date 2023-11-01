using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class VerificationDto : BaseEntity
{
    public required string Code { get; set; }
    
    public UserChannelDto? UserChannel { get; set; }
}
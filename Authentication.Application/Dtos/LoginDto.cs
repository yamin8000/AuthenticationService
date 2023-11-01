using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class LoginDto : BaseEntity
{
    public required string Ip { get; set; }
    public bool IsSuccess { get; set; }
    
    public UserChannelDto? UserChannel { get; set; }
}
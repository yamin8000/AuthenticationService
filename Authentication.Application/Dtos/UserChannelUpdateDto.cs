using Authentication.Domain.Entities;
using Channel = Authentication.Domain.Entities.Channel;

namespace Authentication.Application.Dtos;

public class UserChannelUpdateDto
{
    public Channel? Channel { get; set; }

    public string? Value { get; set; }

    public bool? IsDefault { get; set; }

    public User? User { get; set; }
}
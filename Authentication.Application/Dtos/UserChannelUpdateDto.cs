using Authentication.Domain.Entities;
using Authentication.Domain.Enums;

namespace Authentication.Application.Dtos;

public class UserChannelUpdateDto
{
    public Channel? Channel { get; set; }

    public required string? Value { get; set; }

    public bool? IsDefault { get; set; }

    public User? User { get; set; }
}
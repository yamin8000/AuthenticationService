using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class UserChannelCreateDto
{
    public required string Value { get; set; }
    public required Channel Channel { get; set; }

    public bool IsDefault { get; set; }
}
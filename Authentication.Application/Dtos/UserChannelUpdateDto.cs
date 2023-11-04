using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class UserChannelUpdateDto
{
    public string? Value { get; set; }
    public bool IsDefault { get; set; }
    public User? User { get; set; }
    public Channel? Channel { get; set; }
    public Verification? Verification { get; set; }
}
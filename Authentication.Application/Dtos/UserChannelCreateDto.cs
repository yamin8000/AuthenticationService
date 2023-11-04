namespace Authentication.Application.Dtos;

public class UserChannelCreateDto
{
    public required int ChannelId { get; set; }

    public required string Value { get; set; }
}
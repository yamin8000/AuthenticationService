namespace Authentication.Application.Dtos;

public class UserChannelUpdateDto
{
    public required string Code { get; set; }

    public required int ChannelId { get; set; }
}
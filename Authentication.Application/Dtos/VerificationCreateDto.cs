namespace Authentication.Application.Dtos;

public class VerificationCreateDto
{
    public required string Code { get; set; }

    public required Guid UserChannelId { get; set; }
}
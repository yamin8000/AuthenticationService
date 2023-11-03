using Authentication.Domain.Enums;

namespace Authentication.Application.Dtos;

public class CreateVerificationDto
{
    public required string Channel { get; set; }

    public required string Value { get; set; }
}
using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class UpdateVerificationDto
{
    public required string Code { get; set; }
}
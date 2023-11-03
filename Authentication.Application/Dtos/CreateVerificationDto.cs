namespace Authentication.Application.Dtos;

/// <summary>
/// Verification Request DTO
/// </summary>
public class CreateVerificationDto
{
    public required string Channel { get; set; }

    public required string Value { get; set; }

    public bool IsResending { get; set; } = false;

    public bool IsSettingAsDefault { get; set; } = false;
}
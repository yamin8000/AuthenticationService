using Authentication.Domain.Enums;

namespace Authentication.Domain.Entities;

public class UserChannelDto : SafeDeleteEntity
{
    public Channel Channel { get; set; }
    public required string Value { get; set; }
    public bool IsDefault { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    public int? VerificationId { get; set; }
    public ICollection<VerificationDto> Verifications { get; set; } = new List<VerificationDto>();

    public ICollection<PasswordResetDto> PasswordResets { get; set; } = new List<PasswordResetDto>();
    public ICollection<LoginDto> Logins { get; set; } = new List<LoginDto>();
}
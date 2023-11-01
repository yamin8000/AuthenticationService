using Authentication.Domain.Entities;
using Authentication.Domain.Enums;

namespace Authentication.Application.Dtos;

public class UserChannelDto : SafeDeleteEntity
{
    public Channel Channel { get; set; }
    public required string Value { get; set; }
    public bool IsDefault { get; set; }
    
    public User? User { get; set; }
    
    public ICollection<VerificationDto> Verifications { get; set; } = new List<VerificationDto>();
    public ICollection<PasswordResetDto> PasswordResets { get; set; } = new List<PasswordResetDto>();
    public ICollection<LoginDto> Logins { get; set; } = new List<LoginDto>();
}
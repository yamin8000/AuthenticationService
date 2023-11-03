namespace Authentication.Domain.Entities;

public class UserChannel : BaseEntity
{
    public required string Value { get; set; }
    public bool IsDefault { get; set; }

    public User? User { get; set; }

    // ReSharper disable once UnusedMember.Global
    public int ChannelId { get; set; }
    public required Channel Channel { get; set; }

    public required Guid VerificationId { get; set; }

    public IEnumerable<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();
    public IEnumerable<Login> Logins { get; set; } = new List<Login>();
}
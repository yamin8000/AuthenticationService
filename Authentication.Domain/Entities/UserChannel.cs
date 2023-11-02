namespace Authentication.Domain.Entities;

public class UserChannel : BaseEntity
{
    public required string Value { get; set; }
    public bool IsDefault { get; set; }

    public required Channel Channel { get; set; }
    public User? User { get; set; }

    public ICollection<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();
    public ICollection<Login> Logins { get; set; } = new List<Login>();
}
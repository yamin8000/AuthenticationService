using Microsoft.EntityFrameworkCore;

namespace Authentication.Domain.Entities;

[Index(nameof(Username), IsUnique = true)]
public class User : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }

    public IEnumerable<UserChannel> Channels { get; set; } = new List<UserChannel>();

    public required Guid VerificationId { get; set; }
}
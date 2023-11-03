using Microsoft.EntityFrameworkCore;

namespace Authentication.Domain.Entities;

[Index(nameof(UserChannel), IsUnique = true)]
public class Verification : BaseEntity
{
    public required string Code { get; set; }

    public UserChannel? UserChannel { get; set; }
}
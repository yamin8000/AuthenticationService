namespace Authentication.Domain.Entities;

public class Verification : BaseEntity
{
    public required string Code { get; set; }

    public bool IsVerified { get; set; }

    public UserChannel? UserChannel { get; set; }
}
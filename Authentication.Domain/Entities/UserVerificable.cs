namespace Authentication.Domain.Entities;

public abstract class UserVerificable : SafeDeleteEntity
{
    public required int UserId { get; set; }
    public bool IsDefault { get; set; } = false;
    public int? VerificationId { get; set; }
}
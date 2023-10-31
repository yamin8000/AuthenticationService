namespace Authentication.Domain.Entities;

public class UserEmail : UserVerificable
{
    public required string Email { get; set; }
}
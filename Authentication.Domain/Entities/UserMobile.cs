namespace Authentication.Domain.Entities;

public class UserMobile : UserVerificable
{
    public required int Mobile { get; set; }
}
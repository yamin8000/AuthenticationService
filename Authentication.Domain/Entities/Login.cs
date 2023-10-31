namespace Authentication.Domain.Entities;

public class Login : BaseEntity
{
    public required int UserId { get; set; }
    public required string Ip { get; set; }
    public bool IsSuccess { get; set; } = false;
}
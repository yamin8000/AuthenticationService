namespace Authentication.Application.Dtos;

public class UserDto : SafeDeleteDto
{
    public required string Username { get; set; }
}
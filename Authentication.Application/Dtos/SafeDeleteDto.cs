namespace Authentication.Application.Dtos;

public class SafeDeleteDto : BaseDto
{
    public DateTime? DeletedAt { get; set; }
}
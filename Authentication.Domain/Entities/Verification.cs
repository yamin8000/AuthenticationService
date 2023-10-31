using Authentication.Domain.Enums;

namespace Authentication.Domain.Entities;

public class Verification : BaseEntity
{
    public required int UserId { get; set; }
    public required Channel Channel { get; set; }
    public string? Code { get; set; }
}
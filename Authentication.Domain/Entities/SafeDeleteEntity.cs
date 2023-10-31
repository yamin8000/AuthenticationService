namespace Authentication.Domain.Entities;

public abstract class SafeDeleteEntity: BaseEntity
{
    public DateTime? DeletedAt { get; set; }
}
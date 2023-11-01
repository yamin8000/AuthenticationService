using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface ISafeDeleteRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
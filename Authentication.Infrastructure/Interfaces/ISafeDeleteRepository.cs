using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface ISafeDeleteRepository<TEntity>
    : IRepository<TEntity>
    where TEntity : SafeDeleteEntity
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
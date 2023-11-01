using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface ISafeDeleteRepository<T>: IRepository<T> where T : SafeDeleteEntity
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
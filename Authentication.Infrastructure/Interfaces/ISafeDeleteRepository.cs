using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface ISafeDeleteRepository<T>: IRepository<T> where T : SafeDeleteEntity
{
    Task DeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}
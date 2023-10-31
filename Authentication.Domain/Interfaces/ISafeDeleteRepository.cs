using Authentication.Domain.Entities;

namespace Authentication.Domain.Interfaces;

public interface ISafeDeleteRepository<T>: IRepository<T> where T : class
{
    Task DeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}
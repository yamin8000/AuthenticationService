namespace Authentication.Infrastructure.Interfaces;

public interface ICrudRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> CreateAsync(T? entity);
    Task<T?> UpdateAsync(T? entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
    Task<bool> ForceDeleteAsync(Guid id);
}
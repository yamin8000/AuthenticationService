using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task<TEntity?> CreateAsync(TEntity? entity);
    Task<TEntity?> UpdateAsync(TEntity? entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
    Task<bool> ForceDeleteAsync(Guid id);
}
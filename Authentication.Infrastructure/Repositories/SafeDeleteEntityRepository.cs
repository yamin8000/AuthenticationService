using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;

namespace Authentication.Infrastructure.Repositories;

public class SafeDeleteEntityRepository<TEntity> :
    ISafeDeleteRepository<TEntity> where TEntity : SafeDeleteEntity
{
    private readonly AppDbContext _context;
    private readonly EntityRepository<TEntity> _entityRepository;

    protected SafeDeleteEntityRepository(AppDbContext context)
    {
        _context = context;
        _entityRepository = new EntityRepository<TEntity>(_context);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return false;
        entity.DeletedAt = DateTime.Now;
        return await _entityRepository.Save();
    }

    public virtual async Task<bool> RestoreAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return false;
        entity.DeletedAt = null;
        return await _entityRepository.Save();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _entityRepository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await _entityRepository.GetAllAsync();
    }

    public virtual async Task<TEntity?> CreateAsync(TEntity? entity)
    {
        return await _entityRepository.CreateAsync(entity);
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity? entity)
    {
        return await _entityRepository.UpdateAsync(entity);
    }

    public virtual async Task<bool> ForceDeleteAsync(Guid id)
    {
        return await _entityRepository.ForceDeleteAsync(id);
    }
}
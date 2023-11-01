using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;

namespace Authentication.Infrastructure.Repositories;

public class SafeDeleteEntityRepository<TEntity> :
    EntityRepository<TEntity>, ISafeDeleteRepository
    where TEntity : SafeDeleteEntity
{
    private readonly AppDbContext _context;

    private readonly EntityRepository<TEntity> _entityRepository;

    public SafeDeleteEntityRepository(AppDbContext context, EntityRepository<TEntity> entityRepository) : base(context)
    {
        _context = context;
        _entityRepository = entityRepository;
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
}
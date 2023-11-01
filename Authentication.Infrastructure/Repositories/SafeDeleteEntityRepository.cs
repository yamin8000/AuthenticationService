using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;

namespace Authentication.Infrastructure.Repositories;

public class SafeDeleteEntityRepository<T> :
    ISafeDeleteRepository<T> where T : SafeDeleteEntity
{
    private readonly AppDbContext _context;
    private readonly EntityRepository<T> _entityRepository;

    protected SafeDeleteEntityRepository(AppDbContext context)
    {
        _context = context;
        _entityRepository = new EntityRepository<T>(_context);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task RestoreAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = null;
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _entityRepository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entityRepository.GetAllAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _entityRepository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        await _entityRepository.UpdateAsync(entity);
    }

    public virtual async Task ForceDeleteAsync(Guid id)
    {
        await _entityRepository.ForceDeleteAsync(id);
    }
}
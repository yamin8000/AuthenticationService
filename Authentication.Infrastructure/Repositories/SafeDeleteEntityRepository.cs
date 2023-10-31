using Authentication.Domain.Entities;
using Authentication.Domain.Interfaces;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class SafeDeleteEntityRepository<T> :
    ISafeDeleteRepository<T> where T : SafeDeleteEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected SafeDeleteEntityRepository(ApplicationDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task RestoreAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = null;
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task ForceDeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
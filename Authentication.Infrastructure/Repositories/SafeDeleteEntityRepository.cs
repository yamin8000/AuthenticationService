using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class SafeDeleteEntityRepository<T> : EntityRepository<T> where T : SafeDeleteEntity
{
    protected SafeDeleteEntityRepository(ApplicationDbContext context, DbSet<T> dbSet) : base(context, dbSet)
    {
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = DateTime.Now;
            await Context.SaveChangesAsync();
        }
    }

    public virtual async Task Restore(Guid id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity != null)
        {
            entity.DeletedAt = null;
            await Context.SaveChangesAsync();
        }
    }
}
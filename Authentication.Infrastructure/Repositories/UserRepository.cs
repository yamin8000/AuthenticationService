using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class UserRepository : SafeDeleteEntityRepository<User>
{
    protected UserRepository(ApplicationDbContext context, DbSet<User> dbSet) : base(context, dbSet)
    {
    }
}
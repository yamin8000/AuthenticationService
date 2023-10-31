using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class UserChannelRepository : SafeDeleteEntityRepository<UserChannel>
{
    protected UserChannelRepository(ApplicationDbContext context, DbSet<UserChannel> dbSet) : base(context, dbSet)
    {
    }
}
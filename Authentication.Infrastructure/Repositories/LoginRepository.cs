using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class LoginRepository : EntityRepository<Login>
{
    protected LoginRepository(ApplicationDbContext context, DbSet<Login> dbSet) : base(context, dbSet)
    {
    }
}
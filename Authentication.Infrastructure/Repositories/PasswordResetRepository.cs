using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class PasswordResetRepository : BaseEntityRepository<PasswordReset>
{
    protected PasswordResetRepository(ApplicationDbContext context, DbSet<PasswordReset> dbSet) : base(context, dbSet)
    {
    }
}
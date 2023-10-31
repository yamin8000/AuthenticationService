using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class VerificationRepository : BaseEntityRepository<Verification>
{
    protected VerificationRepository(ApplicationDbContext context, DbSet<Verification> dbSet) : base(context, dbSet)
    {
    }
}
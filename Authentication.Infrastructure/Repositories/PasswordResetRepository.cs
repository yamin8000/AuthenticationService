using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class PasswordResetRepository : EntityRepository<PasswordReset>
{
    protected PasswordResetRepository(ApplicationDbContext context) : base(context)
    {
    }
}
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class PasswordResetRepository : EntityRepository<PasswordReset>
{
    public PasswordResetRepository(ApplicationDbContext context) : base(context)
    {
    }
}
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class VerificationRepository : EntityRepository<Verification>
{
    public VerificationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
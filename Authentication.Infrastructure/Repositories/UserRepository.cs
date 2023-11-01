using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class UserRepository : SafeDeleteEntityRepository<User>
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
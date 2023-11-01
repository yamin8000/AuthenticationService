using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class UserChannelRepository : SafeDeleteEntityRepository<UserChannel>
{
    public UserChannelRepository(ApplicationDbContext context) : base(context)
    {
    }
}
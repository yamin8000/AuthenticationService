using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Seeders;

public class Seeder<TEntity> where TEntity: BaseEntity
{
    private readonly ModelBuilder _modelBuilder;
    
    
}
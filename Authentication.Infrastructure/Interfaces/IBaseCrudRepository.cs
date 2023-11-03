using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;

public interface IBaseCrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEntity
{
}
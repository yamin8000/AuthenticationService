using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface ISafeDeleteCrudService<TEntity, in TCreateDto, in TUpdateDto>
    : ICrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
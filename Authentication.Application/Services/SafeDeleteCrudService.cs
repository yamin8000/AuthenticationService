using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class SafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto>
    : CrudService<TEntity, TCreateDto, TUpdateDto>,
        ISafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly ISafeDeleteRepository<TEntity> _repository;

    public SafeDeleteCrudService(ISafeDeleteRepository<TEntity> repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        return await _repository.RestoreAsync(id);
    }
}
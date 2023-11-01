using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class SafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto>
    : CrudService<TEntity, TCreateDto, TUpdateDto>, ISafeDeleteCrudService
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly ISafeDeleteRepository _safeDeleteRepository;

    protected SafeDeleteCrudService(ISafeDeleteRepository safeDeleteRepository, IRepository<TEntity> repository)
        : base(repository)
    {
        _safeDeleteRepository = safeDeleteRepository;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _safeDeleteRepository.DeleteAsync(id);
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        return await _safeDeleteRepository.RestoreAsync(id);
    }
}
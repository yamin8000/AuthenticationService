using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public abstract class SafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto>
    : ISafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly CrudService<TEntity, TCreateDto, TUpdateDto> _crudService;
    private readonly ISafeDeleteRepository<TEntity> _repository;

    protected SafeDeleteCrudService(
        CrudService<TEntity, TCreateDto, TUpdateDto> crudService,
        ISafeDeleteRepository<TEntity> repository)
    {
        _crudService = crudService;
        _repository = repository;
    }

    public async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await _crudService.GetAllAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _crudService.GetByIdAsync(id);
    }

    public abstract Task<TEntity> CreateAsync(TCreateDto createDto);

    public abstract Task<TEntity> UpdateAsync(Guid id, TUpdateDto updateDto);

    public async Task<bool> ForceDeleteAsync(Guid id)
    {
        return await _crudService.ForceDeleteAsync(id);
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
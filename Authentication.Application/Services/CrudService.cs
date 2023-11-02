using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public abstract class CrudService<TEntity, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly IRepository<TEntity> _repository;

    protected CrudService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public abstract Task<TEntity?> CreateAsync(TCreateDto createDto);

    public abstract Task<TEntity?> UpdateAsync(Guid id, TUpdateDto updateDto);

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        return await _repository.RestoreAsync(id);
    }
    
    public virtual async Task<bool> ForceDeleteAsync(Guid id)
    {
        return await _repository.ForceDeleteAsync(id);
    }
}
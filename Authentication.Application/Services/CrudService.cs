using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Repositories;

namespace Authentication.Application.Services;

public class CrudService<TEntity, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly IRepository<TEntity> _repository;

    public CrudService(IRepository<TEntity> repository)
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

    public virtual Task<TEntity?> CreateAsync(TCreateDto createDto)
    {
        return null;
    }

    public virtual Task<TEntity?> UpdateAsync(Guid id, TUpdateDto updateDto)
    {
        return null;
    }

    public virtual async Task<bool> ForceDeleteAsync(Guid id)
    {
        return await _repository.ForceDeleteAsync(id);
    }
}
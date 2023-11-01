using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

public class SafeDeleteCrudController<TEntity, TCreateDto, TUpdateDto>
    : CrudController<TEntity, TCreateDto, TUpdateDto>,
        ISafeDeleteCrudController<TCreateDto, TUpdateDto>
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{

    private readonly SafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto> _service;
    public SafeDeleteCrudController(SafeDeleteCrudService<TEntity, TCreateDto, TUpdateDto> service) : base(service)
    {
        _service = service;
    }

    [HttpPost("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _service.DeleteAsync(id));
    }

    [HttpPost("restore/{id:guid}")]
    public async Task<IActionResult> Restore(Guid id)
    {
        return Ok(await _service.RestoreAsync(id));
    }
}
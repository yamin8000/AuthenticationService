using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

public class SafeDeleteCrudController<TEntity, TCreateDto, TUpdateDto>
    : CrudController<TEntity, TCreateDto, TUpdateDto>, ISafeDeleteCrudController
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    public SafeDeleteCrudController(ICrudService<TEntity, TCreateDto, TUpdateDto> service) : base(service)
    {
    }

    [HttpPost("delete/{id:guid}")]
    public Task<IActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("restore/{id:guid}")]
    public Task<IActionResult> Restore(Guid id)
    {
        throw new NotImplementedException();
    }
}
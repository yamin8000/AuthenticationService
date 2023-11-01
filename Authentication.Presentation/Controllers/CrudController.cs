using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController<TEntity, TCreateDto, TUpdateDto>
    : ControllerBase, ICrudController<TCreateDto, TUpdateDto>
    where TEntity : SafeDeleteEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly ICrudService<TEntity, TCreateDto, TUpdateDto> _service;

    public CrudController(ICrudService<TEntity, TCreateDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpGet]
    public Task<IActionResult> GetAllAsync()
    {
        return Task.FromResult<IActionResult>(Ok(_service.GetAllAsync()));
    }

    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> CreateAsync([FromBody] TCreateDto createDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public Task<IActionResult> UpdateAsync(Guid id, [FromBody] TUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task<IActionResult> ForceDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController<TEntity, TCreateDto, TUpdateDto>
    : ControllerBase, ICrudController<TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly ICrudService<TEntity, TCreateDto, TUpdateDto> _service;

    public CrudController(ICrudService<TEntity, TCreateDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TCreateDto createDto)
    {
        return Ok(await _service.CreateAsync(createDto));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TUpdateDto updateDto)
    {
        return Ok(await _service.UpdateAsync(id, updateDto));
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ForceDeleteAsync(Guid id)
    {
        return Ok(await _service.ForceDeleteAsync(id));
    }
}
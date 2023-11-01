using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController<T> : ControllerBase where T : BaseEntity
{
    private readonly IRepository<T> _repository;

    public CrudController(IRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }
}
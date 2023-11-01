using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UserController(IRepository<User> lRepository)
    {
        _repository = lRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }
}
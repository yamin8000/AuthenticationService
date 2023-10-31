using Authentication.Domain.Entities;
using Authentication.Domain.Interfaces;
using Authentication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IRepository<Login> _repository;

    public LoginController(ISafeDeleteRepository<Login> lRepository)
    {
        _repository = lRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
    {
        return Ok(await new ApplicationDbContext().Logins.ToListAsync());
    }
}
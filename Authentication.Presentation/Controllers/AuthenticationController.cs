using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [ProducesResponseType(typeof(UserChannel), 200)]
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(UserChannelCreateDto userChannelCreateDto)
    {
        return Ok(await _authService.Register(userChannelCreateDto));
    }

    [HttpPost("Verify")]
    public async Task<IActionResult> Verify(UserChannelUpdateDto userChannelUpdateDto)
    {
        return Ok(await _authService.Verify(userChannelUpdateDto));
    }
}
using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class VerificationController : CrudController<Verification, CreateVerificationDto, UpdateVerificationDto>
{
    public VerificationController(ICrudService<Verification, CreateVerificationDto, UpdateVerificationDto> service) :
        base(service)
    {
    }

    /// <summary>
    /// Request for new Verification, Step 1
    /// </summary>
    /// <param name="createVerificationDto">Verification Request DTO</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(Verification), 200)]
    [HttpPost("Verify")]
    public async Task<IActionResult> SignUp(CreateVerificationDto createVerificationDto)
    {
        return await base.CreateAsync(createVerificationDto);
    }

    /// <summary>
    /// Verify new user, Step 2
    /// </summary>
    /// <param name="id" example="3b6291b6-7f95-4ec1-99e2-bc4a2150bf93">User Channel Id</param>
    /// <param name="updateVerificationDto"></param>
    /// <returns></returns>
    [HttpPut("Verify")]
    public async Task<IActionResult> Verify(Guid id, UpdateVerificationDto updateVerificationDto)
    {
        return await base.UpdateAsync(id, updateVerificationDto);
    }

    public override async Task<IActionResult> CreateAsync(CreateVerificationDto createDto)
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> UpdateAsync(Guid id, UpdateVerificationDto updateDto)
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> GetAllAsync()
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> Delete(Guid id)
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> Restore(Guid id)
    {
        return await Task.Run(Unauthorized);
    }

    public override async Task<IActionResult> ForceDeleteAsync(Guid id)
    {
        return await Task.Run(Unauthorized);
    }
}
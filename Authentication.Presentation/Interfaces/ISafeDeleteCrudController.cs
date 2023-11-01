using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Interfaces;

public interface ISafeDeleteCrudController<in TCreateDto, in TUpdateDto>
    : ICrudController<TCreateDto, TUpdateDto>
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<IActionResult> Delete(Guid id);
    Task<IActionResult> Restore(Guid id);
}
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Presentation.Interfaces;

public interface ISafeDeleteCrudController
{
    Task<IActionResult> Delete(Guid id);
    Task<IActionResult> Restore(Guid id);
}
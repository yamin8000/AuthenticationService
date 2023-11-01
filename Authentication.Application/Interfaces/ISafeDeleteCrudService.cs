using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface ISafeDeleteCrudService
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
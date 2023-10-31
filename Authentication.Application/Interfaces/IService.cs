namespace Authentication.Application.Interfaces;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(object createDto);
    Task UpdateAsync(Guid id, object updateDto);
    Task DeleteAsync(Guid id);
}
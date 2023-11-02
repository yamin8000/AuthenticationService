using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class UserService : CrudService<User, UserCreateDto, UserUpdateDto>
{
    public UserService(IRepository<User> repository) : base(repository)
    {
    }

    public override Task<User?> CreateAsync(UserCreateDto createDto)
    {
        throw new NotImplementedException();
    }

    public override Task<User?> UpdateAsync(Guid id, UserUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}
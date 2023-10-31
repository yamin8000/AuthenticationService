using Authentication.Application.Dtos;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface IUserService : IService<UserDto>
{
}
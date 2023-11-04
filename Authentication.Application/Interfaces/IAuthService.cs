using Authentication.Application.Dtos;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface IAuthService
{
    public Task<UserChannel> Register(UserChannelCreateDto userChannelCreateDto);

    public Task<User> Verify(UserChannelUpdateDto userChannelUpdateDto);
}
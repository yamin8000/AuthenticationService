using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class UserChannelService : CrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>
{
    public UserChannelService(IRepository<UserChannel> repository) : base(repository)
    {
    }

    public override async Task<UserChannel?> CreateAsync(UserChannelCreateDto createDto)
    {
        return await Repository.CreateAsync(new UserChannel
        {
            Channel = createDto.Channel,
            Value = createDto.Value,
            IsDefault = createDto.IsDefault,
            User = createDto.User
        });
    }

    public override async Task<UserChannel?> UpdateAsync(Guid id, UserChannelUpdateDto updateDto)
    {
        var channel = await Repository.GetByIdAsync(id);

        //todo fixing update
        return null;
    }
}
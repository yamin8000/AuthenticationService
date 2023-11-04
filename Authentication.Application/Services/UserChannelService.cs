using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class UserChannelService : CrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>
{
    public UserChannelService(ICrudRepository<UserChannel> repository) : base(repository)
    {
    }

    public override Task<UserChannel?> CreateAsync(UserChannelCreateDto createDto)
    {
        return await Repository.CreateAsync(new UserChannel
        {
            Value = createDto.Value,
        });
    }

    public override Task<UserChannel?> UpdateAsync(Guid id, UserChannelUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}
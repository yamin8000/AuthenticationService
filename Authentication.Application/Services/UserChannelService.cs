using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using static System.String;

namespace Authentication.Application.Services;

public class UserChannelService : CrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>
{
    private readonly IRepository<Channel> _channelRepository;

    public UserChannelService(
        ICrudRepository<UserChannel> repository,
        IRepository<Channel> channelRepository) :
        base(repository)
    {
        _channelRepository = channelRepository;
    }

    public override async Task<UserChannel?> CreateAsync(UserChannelCreateDto createDto)
    {
        var channel = await _channelRepository.GetByIdAsync(createDto.ChannelId);
        if (channel != null)
        {
            return await Repository.CreateAsync(new UserChannel
            {
                Value = createDto.Value,
                Channel = channel
            });
        }

        var names = Enum.GetNames(typeof(Domain.Enums.Channel));
        throw new ArgumentException($"Channel must be one of these values: {Join(", ", names)}");
    }

    public override async Task<UserChannel?> UpdateAsync(Guid id, UserChannelUpdateDto updateDto)
    {
        var userChannel = await Repository.GetByIdAsync(id);
        if (userChannel == null)
            throw new Exception($"There's not UserChannel with id: \"{id}\"");
        userChannel.IsDefault = updateDto.IsDefault;
        if (updateDto.Verification != null)
            userChannel.Verification = updateDto.Verification;
        if (updateDto.Channel != null)
            userChannel.Channel = updateDto.Channel;
        if (updateDto.Value != null)
            userChannel.Value = updateDto.Value;
        if (updateDto.User != null)
            userChannel.User = updateDto.User;
        return await Repository.UpdateAsync(userChannel);
    }
}
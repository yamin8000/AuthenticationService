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
        var userChannel = await _channelRepository.GetByIdAsync(createDto.ChannelId);
        if (userChannel != null)
        {
            return await Repository.CreateAsync(new UserChannel
            {
                Value = createDto.Value,
                Channel = userChannel
            });
        }

        var names = Enum.GetNames(typeof(Domain.Enums.Channel));
        throw new ArgumentException($"Channel must be one of these values: {Join(", ", names)}");
    }

    public override Task<UserChannel?> UpdateAsync(Guid id, UserChannelUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}
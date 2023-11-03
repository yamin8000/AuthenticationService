using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class VerificationService : CrudService<Verification, CreateVerificationDto, UpdateVerificationDto>
{
    private readonly IBaseCrudRepository<UserChannel> _userChannelRepository;
    private readonly ICrudRepository<Channel> _channelRepository;

    public VerificationService(
        IBaseCrudRepository<Verification> repository,
        IBaseCrudRepository<UserChannel> userChannelRepository,
        ICrudRepository<Channel> channelRepository) : base(repository)
    {
        _userChannelRepository = userChannelRepository;
        _channelRepository = channelRepository;
    }

    public override async Task<Verification?> CreateAsync(CreateVerificationDto createVerificationDto)
    {
        var oldUserChannel = _userChannelRepository.GetAllAsync().Result.FirstOrDefault(channel =>
            channel?.Channel.Name == createVerificationDto.Channel &&
            channel.Value == createVerificationDto.Value, null);
        return await (oldUserChannel == null
            ? HandleNewVerification(createVerificationDto)
            : HandleIncompleteVerification(createVerificationDto));
    }

    public override async Task<Verification?> UpdateAsync(Guid id, UpdateVerificationDto updateVerificationDto)
    {
        var channel = await Repository.GetByIdAsync(id);

        //todo fixing update
        return null;
    }

    private async Task<Verification?> HandleNewVerification(CreateVerificationDto createVerificationDto)
    {
        var channel = _channelRepository.GetAllAsync().Result
            .FirstOrDefault(c => c?.Name == createVerificationDto.Channel, null);
        if (channel == null)
        {
            //todo
            //throw exception for handling values other call,sms,email
            return null;
        }

        var userChannel = await _userChannelRepository.CreateAsync(new UserChannel
        {
            Channel = channel,
            Value = createVerificationDto.Value
        });
        var verification = await Repository.CreateAsync(new Verification
        {
            UserChannel = userChannel,
            Code = "123456"
        });
        return verification;
    }

    private async Task<Verification?> HandleIncompleteVerification(CreateVerificationDto createVerificationDto)
    {
        throw new NotImplementedException();
    }
}
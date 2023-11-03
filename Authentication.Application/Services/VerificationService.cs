using Authentication.Application.Dtos;
using Authentication.Application.Utility;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using static System.String;

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
            channel?.ChannelId == FindChannelByString(createVerificationDto.Channel)?.Id &&
            channel?.Value == createVerificationDto.Value, null);
        return await (oldUserChannel == null
            ? HandleNewVerification(createVerificationDto)
            : HandleVerification(oldUserChannel));
    }

    public override async Task<Verification?> UpdateAsync(Guid id, UpdateVerificationDto updateVerificationDto)
    {
        var verification = await Repository.GetByIdAsync(id);
        if (verification == null)
        {
            throw new ArgumentException($"There's no such Verification with id: {id}");
        }

        //todo fixing update
        return null;
    }

    private async Task<Verification?> HandleNewVerification(CreateVerificationDto createVerificationDto)
    {
        var verification = await CreateNewVerification();
        verification.UserChannel = await CreateNewUserChannel(createVerificationDto, verification.Id);
        return verification;
    }

    private async Task<Verification?> HandleVerification(UserChannel userChannel)
    {
        var verification = await CreateNewVerification();
        verification.UserChannel = userChannel;
        return verification;
    }

    private async Task<UserChannel?> CreateNewUserChannel(
        CreateVerificationDto createVerificationDto,
        Guid verificationId)
    {
        var channel = FindChannelByString(createVerificationDto.Channel);
        if (channel != null)
            return await _userChannelRepository.CreateAsync(new UserChannel
            {
                Channel = channel,
                Value = createVerificationDto.Value,
                VerificationId = verificationId
            });

        var names = Enum.GetNames(typeof(Domain.Enums.Channel));
        throw new ArgumentException($"Channel must be one of these values: {Join(", ", names)}");
    }

    private async Task<Verification> CreateNewVerification()
    {
        var verification = await Repository.CreateAsync(new Verification
        {
            Code = Helpers.GenerateVerificationCode()
        });
        return verification ?? throw new Exception("Failed to create Verification database record");
    }


    private Channel? FindChannelByString(string value)
    {
        return _channelRepository.GetAllAsync()
            .Result
            .FirstOrDefault(channel => channel?.Name == value, null);
    }
}
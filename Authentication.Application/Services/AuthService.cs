using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Utility;
using Authentication.Domain.Entities;

namespace Authentication.Application.Services;

public class AuthService : IService
{
    private readonly ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> _userChannelService;
    private readonly ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> _verificationService;

    public AuthService(
        ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> userChannelService,
        ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> verificationService)
    {
        _userChannelService = userChannelService;
        _verificationService = verificationService;
    }

    public async Task<UserChannel> Register(UserChannelCreateDto userChannelCreateDto)
    {
        var oldUserChannel = _userChannelService.GetAllAsync().Result.FirstOrDefault(userChannel =>
            userChannel?.Channel.Id == userChannelCreateDto.ChannelId &&
            userChannel?.Value == userChannelCreateDto.Value, null);
        var userChannel = await (oldUserChannel == null
            ? HandleNewRegister(userChannelCreateDto)
            : HandleOldRegister(oldUserChannel, userChannelCreateDto));
        await SendCodeToChannel(userChannel, userChannelCreateDto);
        return userChannel;
    }

    private async Task<UserChannel> HandleNewRegister(UserChannelCreateDto userChannelCreateDto)
    {
        var userChannel = await _userChannelService.CreateAsync(userChannelCreateDto);
        if (userChannel == null) throw new Exception("Failed to create UserChannel DB record");
        var verification = await _verificationService.CreateAsync(new VerificationCreateDto
        {
            Code = Helpers.GenerateVerificationCode(),
            UserChannelId = userChannel.Id
        });
        if (verification == null)
        {
            throw new Exception(
                $"Failed to create Verification DB record for UserChannel with id:{userChannel.Id}");
        }

        return userChannel;

    }

    private async Task SendCodeToChannel(UserChannel userChannel, UserChannelCreateDto userChannelCreateDto)
    {
        throw new NotImplementedException();
    }

    private Task<UserChannel> HandleOldRegister(UserChannel oldUserChannel, UserChannelCreateDto userChannelCreateDto)
    {
        throw new NotImplementedException();
    }
}
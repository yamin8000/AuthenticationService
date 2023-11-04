using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Utility;
using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Authentication.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfigurationRoot _configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    private readonly HttpClient _httpClient;

    private readonly ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> _userChannelService;
    private readonly ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> _verificationService;

    public AuthService(
        ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> userChannelService,
        ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> verificationService,
        HttpClient httpClient)
    {
        _userChannelService = userChannelService;
        _verificationService = verificationService;
        _httpClient = httpClient;
    }

    public async Task<UserChannel> Register(UserChannelCreateDto userChannelCreateDto)
    {
        var oldUserChannel = _userChannelService.GetAllAsync().Result.FirstOrDefault(userChannel =>
            userChannel?.Channel.Id == userChannelCreateDto.ChannelId &&
            userChannel.Value == userChannelCreateDto.Value, null);
        var userChannel = await (oldUserChannel == null
            ? HandleNewRegister(userChannelCreateDto)
            : HandleOldRegister(oldUserChannel));
        await SendCodeToChannel(userChannel);
        return userChannel;
    }

    public Task<User> Verify(UserChannelUpdateDto userChannelUpdateDto)
    {
        throw new NotImplementedException();
    }

    private async Task<UserChannel> HandleNewRegister(UserChannelCreateDto userChannelCreateDto)
    {
        var userChannel = await _userChannelService.CreateAsync(userChannelCreateDto);
        if (userChannel == null) throw new Exception("Failed to create UserChannel DB record");

        var updatedUserChannel = await _userChannelService.UpdateAsync(userChannel.Id, new UserChannelUpdateDto
        {
            Verification = await CreateVerificationRecord(userChannel.Id)
        });
        if (updatedUserChannel == null)
        {
            throw new Exception("Failed to create UserChannel DB record.");
        }

        return updatedUserChannel;
    }

    private async Task<UserChannel> HandleOldRegister(UserChannel oldUserChannel)
    {
        var verification = _verificationService.GetAllAsync().Result.LastOrDefault(v =>
                v?.Id == oldUserChannel.Verification?.Id, null
        );
        if (verification == null)
        {
            throw new Exception($"Failed to find Verification record for UserChannel with id: \"{oldUserChannel.Id}\"");
        }

        CheckVerificationInterval(verification);

        var updatedUserChannel = await _userChannelService.UpdateAsync(oldUserChannel.Id, new UserChannelUpdateDto
        {
            Verification = await UpdateVerificationRecord(verification, Helpers.GenerateVerificationCode())
        });
        if (updatedUserChannel == null)
        {
            throw new Exception("Failed to create UserChannel DB record.");
        }

        return updatedUserChannel;
    }

    private async Task<Verification> CreateVerificationRecord(Guid userChannelId)
    {
        return await _verificationService.CreateAsync(new VerificationCreateDto
        {
            Code = Helpers.GenerateVerificationCode(),
            UserChannelId = userChannelId
        }) ?? throw new Exception("Failed to create Verification database record.");
    }

    private async Task<Verification> UpdateVerificationRecord(Verification verification, string code)
    {
        return await _verificationService.UpdateAsync(verification.Id, new VerificationUpdateDto
        {
            Code = code
        }) ?? throw new Exception("Failed to update Verification database record.");
    }

    private void CheckVerificationInterval(Verification verification)
    {
        var interval = (DateTime.UtcNow - verification.UpdatedAt).TotalSeconds;
        if (interval <= Constants.SmsTimer)
            throw new Exception(
                $"{interval} seconds from {Constants.SmsTimer} seconds, Verification with id: {verification.Id} is not expired yet!");
    }

    private async Task SendCodeToChannel(UserChannel userChannel)
    {
        var name = userChannel.Channel.Name;
        if (name == Domain.Enums.Channel.Email.ToString())
        {
            throw new NotImplementedException();
        }

        if (name == Domain.Enums.Channel.Sms.ToString())
        {
            await HandleSmsChannel(userChannel);
        }

        if (name == Domain.Enums.Channel.Call.ToString())
        {
            throw new NotImplementedException();
        }
    }

    private async Task HandleSmsChannel(UserChannel userChannel)
    {
        var template = _configuration.GetSection("Kaveh")["Template"];
        var api = _configuration.GetSection("Kaveh")["Api"];
        if (template != null && api != null)
        {
            var uri = CreateKavehSmsNegarUri(userChannel, api, template);
            var response = await _httpClient.GetAsync(uri);
            Console.WriteLine($"SMS Request HTTP Code: {response.StatusCode}");
        }
        else throw new Exception("Kaveh Negar information is empty in appsettings.json!");
    }

    private static string CreateKavehSmsNegarUri(UserChannel userChannel, string? api, string? template)
    {
        return
            $"https://api.kavenegar.com/v1/{api}/verify/lookup.json?template={template}&receptor={userChannel.Value}&token={userChannel.Verification.Code}";
    }
}
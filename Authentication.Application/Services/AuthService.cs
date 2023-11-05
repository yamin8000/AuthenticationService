using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Utility;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.String;

namespace Authentication.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfigurationRoot _configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    private readonly HttpClient _httpClient;

    private readonly ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> _userChannelService;
    private readonly ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> _verificationService;
    private readonly ICrudService<User, UserCreateDto, UserUpdateDto> _userService;

    private readonly IRepository<Channel> _channelRepository;

    public AuthService(ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> userChannelService,
        ICrudService<Verification, VerificationCreateDto, VerificationUpdateDto> verificationService,
        IRepository<Channel> channelRepository,
        ICrudService<User, UserCreateDto, UserUpdateDto> userService,
        HttpClient httpClient)
    {
        _userChannelService = userChannelService;
        _verificationService = verificationService;
        _httpClient = httpClient;
        _userService = userService;
        _channelRepository = channelRepository;
    }

    public async Task<UserChannel> Register(SignUpDto signUpDto)
    {
        var oldUserChannel = _userChannelService.GetAllAsync().Result.FirstOrDefault(userChannel =>
            userChannel?.Channel.Id == signUpDto.ChannelId &&
            userChannel.Value == signUpDto.Value, null);
        var userChannel = await (oldUserChannel == null
            ? HandleNewRegister(signUpDto)
            : HandleOldRegister(oldUserChannel));
        await SendCodeToChannel(userChannel);
        return userChannel;
    }

    public async Task<UserChannel> Verify(VerifyDto verifyDto)
    {
        var userChannel = await _userChannelService.GetByIdAsync(verifyDto.UserChannelId);
        var verification = userChannel?.Verification;
        if (verification == null)
            throw new Exception(
                $"There's no Verification associated with UserChannel with id: \"{verifyDto.UserChannelId}\"");

        if (userChannel != null && verification.Code == verifyDto.Code)
        {
            if (verification.IsVerified)
                throw new Exception($"Verification with id: \"{verification.Id}\" is already verified.");

            await VerifyUserChannel(userChannel);
            return userChannel;
        }

        throw new ArgumentException(
            $"Code: {verifyDto.Code} is an incorrect code for UserChannel with id: \"{verifyDto.UserChannelId}\"");
    }

    public async Task<User> SetCredential(CredentialDto credentialDto)
    {
        var userChannel = await _userChannelService.GetByIdAsync(credentialDto.UserChannelId);
        if (userChannel == null)
            throw new ArgumentException($"There's no UserChannel with id: \"{credentialDto.UserChannelId}\"");
        var verification = userChannel.Verification;
        if (verification == null)
            throw new ArgumentException($"UserChannel with id: \"{userChannel.Id}\" has no Verification.");
        if (!verification.IsVerified)
            throw new ArgumentException(
                $"UserChannel with id: \"{userChannel.Id}\" and Verification with id: \"{verification.Id}\" is not verified.");
        var user = await _userService.CreateAsync(new UserCreateDto
        {
            Username = credentialDto.Username,
            Password = credentialDto.Password,
            VerificationId = verification.Id
        });
        await _userChannelService.UpdateAsync(userChannel.Id, new UserChannelUpdateDto
        {
            User = user
        });
        return user ?? throw new Exception(
            $"Failed to create User for UserChannel with id: \"{userChannel.Id}\" and Verification with id: \"{verification.Id}\"");
    }

    private async Task<Verification?> VerifyUserChannel(UserChannel userChannel)
    {
        var verification = userChannel.Verification;
        if (verification != null)
        {
            if (IsVerificationValid(verification))
            {
                return await _verificationService.UpdateAsync(
                    verification.Id,
                    new VerificationUpdateDto
                    {
                        IsVerified = true
                    }
                );
            }
            throw new ArgumentException($"Verification with id: \"{verification.Id}\" is expired!");
        }

        throw new Exception($"UserChannel with id: \"{userChannel.Id}\" has no Verification.");
    }

    private async Task<UserChannel> HandleNewRegister(SignUpDto signUpDto)
    {
        var channel = await _channelRepository.GetByIdAsync(signUpDto.ChannelId);
        if (channel != null)
        {
            var userChannel = await _userChannelService.CreateAsync(new UserChannelCreateDto
            {
                Channel = channel,
                Value = signUpDto.Value
            });
            if (userChannel == null) throw new Exception("Failed to create UserChannel DB record");

            return await UpdateUserChannelRecord(userChannel, await CreateVerificationRecord());
        }

        var names = Enum.GetNames(typeof(Domain.Enums.Channel));
        throw new ArgumentException($"Channel must be one of these values: {Join(", ", names)}");
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
        return await UpdateUserChannelRecord(
            oldUserChannel,
            await UpdateVerificationRecord(verification, Helpers.GenerateVerificationCode())
        );
    }

    private async Task<UserChannel> UpdateUserChannelRecord(UserChannel userChannel, Verification verification)
    {
        var updatedUserChannel = await _userChannelService.UpdateAsync(userChannel.Id, new UserChannelUpdateDto
        {
            Verification = verification
        });
        if (updatedUserChannel == null)
        {
            throw new Exception("Failed to update UserChannel DB record.");
        }

        return updatedUserChannel;
    }

    private async Task<Verification> CreateVerificationRecord()
    {
        return await _verificationService.CreateAsync(new VerificationCreateDto
        {
            Code = Helpers.GenerateVerificationCode()
        }) ?? throw new Exception("Failed to create Verification database record.");
    }

    private async Task<Verification> UpdateVerificationRecord(Verification verification, string code)
    {
        return await _verificationService.UpdateAsync(verification.Id, new VerificationUpdateDto
        {
            Code = code
        }) ?? throw new Exception("Failed to update Verification database record.");
    }

    private static void CheckVerificationInterval(Verification verification)
    {
        if (IsVerificationValid(verification))
            throw new Exception($"Verification with id: {verification.Id} is not expired yet!");
    }

    private static bool IsVerificationValid(Verification verification)
    {
        return (DateTime.UtcNow - verification.UpdatedAt).TotalSeconds <= Constants.SmsTimer;
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
        var template = _configuration.GetSection("SMS-SDK")["Template"];
        var api = _configuration.GetSection("SMS-SDK")["Api"];
        if (template != null && api != null)
        {
            var uri = CreateSmsUri(userChannel, api, template);
            var response = await _httpClient.GetAsync(uri);
            Console.WriteLine($"SMS Request HTTP Code: {response.StatusCode}");
        }
        else throw new Exception("SMS SDK information is empty in appsettings.json!");
    }

    private static string CreateSmsUri(UserChannel userChannel, string? api, string? template)
    {
        return
            $"https://api.kavenegar.com/v1/{api}/verify/lookup.json?template={template}&receptor={userChannel.Value}&token={userChannel.Verification?.Code}";
    }
}
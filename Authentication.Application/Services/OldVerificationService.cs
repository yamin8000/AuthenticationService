using Authentication.Application.Dtos;
using Authentication.Application.Utility;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.String;

namespace Authentication.Application.Services;

public class OldVerificationService : CrudService<Verification, VerificationCreateDto, VerificationUpdateDto>
{
    private readonly ICrudRepository<UserChannel> _userChannelRepository;
    private readonly ICrudRepository<Channel> _channelRepository;
    private readonly ICrudRepository<Verification> _verificationRepository;

    private readonly HttpClient _httpClient;

    private IConfigurationRoot _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    public OldVerificationService(ICrudRepository<Verification> repository,
        ICrudRepository<UserChannel> userChannelRepository,
        ICrudRepository<Channel> channelRepository,
        ICrudRepository<Verification> verificationRepository,
        HttpClient httpClient) : base(repository)
    {
        _userChannelRepository = userChannelRepository;
        _channelRepository = channelRepository;
        _httpClient = httpClient;
        _verificationRepository = verificationRepository;
    }

    public override async Task<Verification?> CreateAsync(VerificationCreateDto verificationCreateDto)
    {
        var oldUserChannel = _userChannelRepository.GetAllAsync().Result.FirstOrDefault(userChannel =>
            userChannel?.Channel.Id == FindChannelByString(verificationCreateDto.Channel)?.Id &&
            userChannel?.Value == verificationCreateDto.Value, null);
        var verification = await (oldUserChannel == null
            ? HandleNewVerification(verificationCreateDto)
            : HandleVerification(oldUserChannel));
        await HandleChannel(verification);
        return verification;
    }

    public override async Task<Verification?> UpdateAsync(Guid id, VerificationUpdateDto verificationUpdateDto)
    {
        var verification = await Repository.GetByIdAsync(id);
        if (verification == null)
        {
            throw new ArgumentException($"There's no such Verification with id: {id}");
        }

        //todo fixing update
        return null;
    }

    private async Task<Verification> HandleNewVerification(VerificationCreateDto verificationCreateDto)
    {
        var verification = await CreateNewVerification();
        verification.UserChannel = await CreateNewUserChannel(verificationCreateDto, verification.Id);
        return verification;
    }

    private async Task<Verification> HandleVerification(UserChannel userChannel)
    {
        var verification = _verificationRepository.GetAllAsync()
            .Result
            .LastOrDefault(v => v?.Id == userChannel.VerificationId, null);

        if (verification == null)
        {
            throw new Exception($"Failed to find Verification with id: {userChannel.VerificationId}");
        }

        var interval = (DateTime.UtcNow - verification.UpdatedAt).TotalSeconds;
        if (interval <= Constants.SmsTimer)
            throw new Exception(
                $"{interval} seconds from {Constants.SmsTimer} seconds, Verification with id: {verification.Id} is not expired yet!");

        verification.Code = Helpers.GenerateVerificationCode();
        verification.UserChannel = userChannel;
        await _verificationRepository.UpdateAsync(verification);
        return verification;
    }

    private async Task<UserChannel> CreateNewUserChannel(
        VerificationCreateDto verificationCreateDto,
        Guid verificationId)
    {
        var channel = FindChannelByString(verificationCreateDto.Channel);
        if (channel != null)
        {
            var userChannel = await _userChannelRepository.CreateAsync(new UserChannel
            {
                Channel = channel,
                Value = verificationCreateDto.Value,
                VerificationId = verificationId
            });
            return userChannel ?? throw new Exception("Failed to create Channel database record");
        }

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

    private async Task HandleChannel(Verification verification)
    {
        var name = verification.UserChannel.Channel.Name;
        if (name == Domain.Enums.Channel.Email.ToString())
        {
            throw new NotImplementedException();
        }

        if (name == Domain.Enums.Channel.Sms.ToString())
        {
            await HandleSmsChannel(verification);
        }

        if (name == Domain.Enums.Channel.Call.ToString())
        {
            throw new NotImplementedException();
        }
    }

    private async Task HandleSmsChannel(Verification verification)
    {
        var template = _configuration.GetSection("Kaveh")["Template"];
        var api = _configuration.GetSection("Kaveh")["Api"];
        if (template != null && api != null)
        {
            var uri = CreateKavehSmsNegarUri(verification, api, template);
            var response = await _httpClient.GetAsync(uri);
            Console.WriteLine($"SMS Request HTTP Code: {response.StatusCode}");
        }
        else throw new Exception("Kaveh Negar information is empty in appsettings.json!");
    }

    private static string CreateKavehSmsNegarUri(Verification verification, string? api, string? template)
    {
        return
            $"https://api.kavenegar.com/v1/{api}/verify/lookup.json?template={template}&receptor={verification.UserChannel.Value}&token={verification.Code}";
    }
}
using Authentication.Application.Dtos;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface IAuthService
{
    public Task<UserChannel> Register(SignUpDto signUpDto);

    public Task<UserChannel> Verify(VerifyDto verifyDto);

    public Task<User> SetCredential(CredentialDto credentialDto);

    public Task<User> Login(LoginDto loginDto);

    public Task<PasswordResetRequestDto> PasswordResetRequest(PasswordResetRequestDto passwordResetRequestDto);

    public Task<User> PasswordResetAction(string token, PasswordResetAction passwordResetAction);
}
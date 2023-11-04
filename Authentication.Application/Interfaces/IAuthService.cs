using Authentication.Application.Dtos;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces;

public interface IAuthService
{
    public Task<UserChannel> Register(SignUpDto signUpDto);

    public Task<UserChannel> Verify(VerifyDto verifyDto);

    public Task<User> SetCredential(CredentialDto credentialDto);
}
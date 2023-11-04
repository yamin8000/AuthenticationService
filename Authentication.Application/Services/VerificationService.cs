using Authentication.Application.Dtos;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public class VerificationService : CrudService<Verification, VerificationCreateDto, VerificationUpdateDto>
{
    public VerificationService(ICrudRepository<Verification> repository) : base(repository)
    {
    }

    public override Task<Verification?> CreateAsync(VerificationCreateDto createDto)
    {
        return Repository.CreateAsync(new Verification
        {
            Code = createDto.Code,
            UserChannelId = createDto.UserChannelId
        });
    }

    public override Task<Verification?> UpdateAsync(Guid id, VerificationUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}
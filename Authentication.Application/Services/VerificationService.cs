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
            IsVerified = createDto.IsVerified
        });
    }

    public override async Task<Verification?> UpdateAsync(Guid id, VerificationUpdateDto updateDto)
    {
        var verification = await Repository.GetByIdAsync(id);
        if (verification == null)
            throw new Exception($"There's no Verification with id: \"{id}\"");
        if (updateDto.Code != null)
            verification.Code = updateDto.Code;
        verification.IsVerified = updateDto.IsVerified;
        return await Repository.UpdateAsync(verification);
    }
}
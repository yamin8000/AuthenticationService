using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;

namespace Authentication.Presentation.Controllers;

public class UserChannelController : CrudController<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>
{
    public UserChannelController(ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto> service) :
        base(service)
    {
    }
}
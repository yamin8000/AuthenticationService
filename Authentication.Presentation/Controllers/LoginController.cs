using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;

namespace Authentication.Presentation.Controllers;

public class LoginController : CrudController<Login, string, string>
{
    public LoginController(ICrudService<Login, string, string> service) : base(service)
    {
    }
}
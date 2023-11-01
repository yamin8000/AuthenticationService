using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Presentation.Controllers;

public class UserController : CrudController<User>
{
    public UserController(IRepository<User> repository) : base(repository)
    {
    }
}
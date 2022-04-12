using Todo.Models;
using Todo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _user;

    public UserController(ILogger<UserController> logger,
    IUserRepository user)
    {
        _logger = logger;
        _user = user;
    }
}

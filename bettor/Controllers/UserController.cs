using Microsoft.AspNetCore.Mvc;
using bettor.Models;
using bettor.Services;

namespace bettor.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService = UserService.GetInstance();

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> GetAll()
    {
        return _userService.GetUsers();
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetUser(id);

        if (user == null)
            return NotFound();

        return user;
    }

    [HttpGet("{id}/account")]
    public ActionResult<Account> GetUsersAccount(int id)
    {
        var user = _userService.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        return user.Account;
    }
}

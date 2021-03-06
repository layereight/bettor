using Microsoft.AspNetCore.Mvc;
using bettor.Models;
using bettor.Services;

namespace bettor.Controllers;

[ApiController]
[Route("bets")]
public class BetController : ControllerBase
{
    private readonly ILogger<BetController> _logger;

    private readonly IBetService _betService;
    private readonly IUserService _userService;

    public BetController(ILogger<BetController> logger, IBetService betService, IUserService userService)
    {
        _logger = logger;
        _betService = betService;
        _userService = userService;
    }

    [HttpGet]
    public IEnumerable<Bet> GetAll()
    {
        return _betService.GetBets();
    }

    [HttpGet("{id}")]
    public ActionResult<Bet> GetBet(int id)
    {
        var bet = _betService.GetBet(id);

        if (bet == null)
        {
            return NotFound();
        }

        return bet;
    }

    [HttpPost]
    public IActionResult PlaceBet(Bet bet)
    {
        var user = _userService.GetUser(bet.UserId);

        if (user == null)
        {
            return BadRequest();
        }

        try
        {
            var createdBet = _betService.PlaceBet(user, bet);
            return CreatedAtAction(nameof(GetBet), new { id = createdBet.Id }, null);
        }
        catch (InvalidStakeException e)
        {
            _logger.LogWarning("Unable to place bet! {}", e);
            return BadRequest();
        }
        catch (InvalidBetNumberException e)
        {
            _logger.LogWarning("Unable to place bet! {}", e);
            return BadRequest();
        }
        catch (InsufficientFundsException e)
        {
            _logger.LogWarning("Unable to place bet! {}", e);
            return BadRequest();
        }
    }

    [HttpGet("{id}/result")]
    public ActionResult<BetResult?> GetBetResult(int id)
    {
        var bet = _betService.GetBet(id);

        if (bet == null)
        {
            return NotFound();
        }

        return bet?.BetResult;
    }
}
